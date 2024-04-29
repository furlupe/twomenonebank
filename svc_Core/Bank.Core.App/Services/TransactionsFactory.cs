using Bank.Common.DateTimeProvider;
using Bank.Common.Money.Converter;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain;
using Bank.Core.Domain.Transactions;
using Bank.Exceptions.WebApiException;

namespace Bank.Core.App.Services;

public class TransactionsFactory(
    IAccountService accountService,
    IDateTimeProvider timeProvider,
    ICurrencyConverter currencyConverter
) : ITransactionsFactory
{
    private async Task CheckAccountOwnerhsip(Common.Transaction transaction)
    {
        if (transaction.SourceId == Guid.Empty || transaction.Transfer?.TargetId == Guid.Empty)
            return;

        var initiatorOwnsSource = await accountService.IsAccountOwnedBy(
            transaction.SourceId,
            transaction.InitiatorId
        );

        if (!initiatorOwnsSource)
            throw NotFoundException.ForModel<Account>(transaction.SourceId);
    }

    public async Task<Transaction> Create(Common.Transaction transaction)
    {
        await CheckAccountOwnerhsip(transaction);

        var sourceAccount =
            transaction.SourceId == Guid.Empty
                ? await accountService.GetMasterAccount()
                : await accountService.GetAccount(transaction.SourceId);

        return transaction.Type switch
        {
            Common.Transaction.TransactionType.BalanceChange
                => CreateBalanceChange(transaction, sourceAccount),
            Common.Transaction.TransactionType.Transfer
                => transaction.Transfer!.Type == Common.Transfer.TransferType.Credit
                    ? await CreateCreditTransfer(transaction, sourceAccount)
                    : await CreateTransfer(transaction, sourceAccount),
            _ => throw new ArgumentException(nameof(transaction.Type))
        };
    }

    protected Transaction CreateBalanceChange(
        Common.Transaction transaction,
        Account sourceAccount
    ) =>
        transaction.BalanceChange!.Type switch
        {
            Common.BalanceChange.BalanceChangeType.Deposit
                => new Deposit(
                    transaction.Value,
                    timeProvider.UtcNow,
                    transaction.IdempotenceKey,
                    sourceAccount,
                    currencyConverter
                ),
            Common.BalanceChange.BalanceChangeType.Withdrawal
                => new Withdrawal(
                    transaction.Value,
                    timeProvider.UtcNow,
                    transaction.IdempotenceKey,
                    sourceAccount,
                    currencyConverter
                ),
            _ => throw new ArgumentException(nameof(transaction.BalanceChange.Type))
        };

    protected async Task<Transaction> CreateTransfer(
        Common.Transaction transaction,
        Account sourceAccount
    )
    {
        var targetAccount =
            transaction.Transfer!.Type == Common.Transfer.TransferType.P2P
                ? await accountService.GetUserDefaultAccount(transaction.Transfer!.TargetId)
                : await accountService.GetAccount(transaction.Transfer!.TargetId);

        return new Transfer(
            transaction.Value,
            timeProvider.UtcNow,
            transaction.IdempotenceKey,
            sourceAccount,
            targetAccount,
            currencyConverter,
            message: transaction.Transfer.Message
        );
    }

    protected async Task<Transaction> CreateCreditTransfer(
        Common.Transaction transaction,
        Account sourceAccount
    ) =>
        transaction.SourceId == Guid.Empty
            ? new CreditIssuance(
                transaction.Value,
                timeProvider.UtcNow,
                transaction.IdempotenceKey,
                sourceAccount,
                await accountService.GetAccount(transaction.Transfer!.TargetId),
                transaction.Transfer.CreditTransfer!.CreditId,
                currencyConverter
            )
            : new CreditPayment(
                transaction.Value,
                timeProvider.UtcNow,
                transaction.IdempotenceKey,
                sourceAccount,
                await accountService.GetMasterAccount(),
                transaction.Transfer!.CreditTransfer!.CreditId,
                currencyConverter
            );
}
