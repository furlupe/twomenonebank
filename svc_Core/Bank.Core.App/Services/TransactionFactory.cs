using Bank.Common.DateTimeProvider;
using Bank.Common.Money.Converter;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain;
using Bank.Core.Domain.Transactions;
using Bank.Exceptions.WebApiException;

namespace Bank.Core.App.Services;

public class TransactionFactory(
    IAccountService accountService,
    IDateTimeProvider timeProvider,
    ICurrencyConverter currencyConverter
) : ITransactionFactory
{
    public async Task<Transaction> Create(Common.Transaction transaction)
    {
        var initiatorOwnsSource = await accountService.IsAccountOwnedBy(
            transaction.SourceAccountId,
            transaction.InitiatorId
        );

        if (!initiatorOwnsSource)
            throw NotFoundException.ForModel<Account>(transaction.SourceAccountId);

        var sourceAccount = await accountService.GetAccount(transaction.SourceAccountId);

        return transaction.Type switch
        {
            Common.Transaction.TransactionType.BalanceChange
                => transaction.BalanceChange!.Type switch
                {
                    Common.BalanceChange.BalanceChangeType.Deposit
                        => new Deposit(
                            transaction.Value,
                            timeProvider.UtcNow,
                            sourceAccount,
                            currencyConverter
                        ),
                    Common.BalanceChange.BalanceChangeType.Withdrawal
                        => new Withdrawal(
                            transaction.Value,
                            timeProvider.UtcNow,
                            sourceAccount,
                            currencyConverter
                        ),
                    Common.BalanceChange.BalanceChangeType.CreditPayment
                        => new CreditPayment(
                            transaction.Value,
                            timeProvider.UtcNow,
                            sourceAccount,
                            transaction.BalanceChange.CreditPayment!.CreditId,
                            currencyConverter
                        ),
                    _ => throw new ArgumentException(nameof(transaction.BalanceChange.Type))
                },
            Common.Transaction.TransactionType.Transfer
                => new Transfer(
                    transaction.Value,
                    timeProvider.UtcNow,
                    sourceAccount,
                    await accountService.GetAccount(transaction.Transfer!.TargetAccountId),
                    currencyConverter
                ),
            _ => throw new ArgumentException(nameof(transaction.Type))
        };
    }
}
