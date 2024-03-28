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
        var initiatorAccountId = transaction.Type switch
        {
            Common.Transaction.TransactionType.BalanceChange
                => transaction.BalanceChange!.TargetAccountId,
            Common.Transaction.TransactionType.Transfer => transaction.Transfer!.SourceAccountId,
            _ => throw new ArgumentException(nameof(transaction.Type))
        };

        if (!await accountService.IsAccountOwnedBy(initiatorAccountId, transaction.InitiatorId))
            throw NotFoundException.ForModel<Account>(initiatorAccountId);

        var initiatorAccount = await accountService.GetAccount(initiatorAccountId);

        return transaction.Type switch
        {
            Common.Transaction.TransactionType.BalanceChange
                => transaction.BalanceChange!.Type switch
                {
                    Common.BalanceChange.BalanceChangeType.Deposit
                        => new Deposit(
                            transaction.Value,
                            timeProvider.UtcNow,
                            initiatorAccount,
                            currencyConverter
                        ),
                    Common.BalanceChange.BalanceChangeType.Withdrawal
                        => new Withdrawal(
                            transaction.Value,
                            timeProvider.UtcNow,
                            initiatorAccount,
                            currencyConverter
                        ),
                    Common.BalanceChange.BalanceChangeType.CreditPayment
                        => new Domain.Transactions.CreditPayment(
                            transaction.Value,
                            timeProvider.UtcNow,
                            initiatorAccount,
                            transaction.BalanceChange.CreditPayment!.CreditId,
                            currencyConverter
                        ),
                    _ => throw new ArgumentException(nameof(transaction.BalanceChange.Type))
                },
            Common.Transaction.TransactionType.Transfer
                => new Domain.Transactions.Transfer(
                    transaction.Value,
                    timeProvider.UtcNow,
                    initiatorAccount,
                    await accountService.GetAccount(transaction.Transfer!.TargetAccountId),
                    currencyConverter
                ),
            _ => throw new ArgumentException(nameof(transaction.Type))
        };
    }
}
