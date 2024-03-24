using System;
using Bank.Attributes.Attributes;
using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public Account(User user, string name, Currency currency)
    {
        User = user;
        Name = name;
        Balance = new(0, currency);
    }

    public string Name { get; protected set; }
    public Money Balance { get; protected set; }
    public User User { get; protected set; }
    public Guid UserId { get; protected set; }
    public List<AccountEvent> Events { get; protected set; } = [];

    public async Task Deposit(Deposit transaction, DateTime now, ICurrencyConverter converter)
    {
        var (nativeValue, foreignValue) = await GetTransactionValues(transaction.Value, converter);
        var @event = new AccountEvent(
            $"Deposited {FormatTransactionValues(nativeValue, foreignValue)}.",
            AccountEventType.BalanceChange,
            now,
            balanceChange: new BalanceChange(
                this,
                nativeValue,
                foreignValue,
                BalanceChangeType.Deposit
            )
        );

        Balance += nativeValue;
        Events.Add(@event);
    }

    public async Task Withdraw(Withdrawal transaction, DateTime now, ICurrencyConverter converter)
    {
        var (nativeValue, foreignValue) = await GetTransactionValues(transaction.Value, converter);
        Validation.Check(
            ExceptionConstants.MsgInvalidValue,
            nativeValue <= Balance,
            "Withdrawal sum exceeds account balance."
        );
        Balance -= nativeValue;

        Events.Add(
            new AccountEvent(
                $"Withdrew {FormatTransactionValues(nativeValue, foreignValue)}.",
                AccountEventType.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    nativeValue,
                    foreignValue,
                    BalanceChangeType.Withdrawal
                )
            )
        );
    }

    public async Task RepayCredit(
        Transactions.CreditPayment transaction,
        DateTime now,
        ICurrencyConverter converter
    )
    {
        var (nativeValue, foreignValue) = await GetTransactionValues(transaction.Value, converter);
        Validation.Check(
            ExceptionConstants.MsgInvalidValue,
            nativeValue <= Balance,
            "Payment sum exceeds account balance."
        );
        Balance -= nativeValue;

        Events.Add(
            new AccountEvent(
                $"Payed {FormatTransactionValues(nativeValue, foreignValue)} for credit.",
                AccountEventType.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    nativeValue,
                    foreignValue,
                    new Events.CreditPayment(transaction.CreditId)
                )
            )
        );
    }

    protected async Task<(Money nativeValue, Money foreignValue)> GetTransactionValues(
        Money transactionValue,
        ICurrencyConverter converter
    )
    {
        Money nativeValue =
            transactionValue.Currency == Balance.Currency
                ? transactionValue
                : await converter.Convert(transactionValue, Balance.Currency);

        return (nativeValue with { }, transactionValue with { });
    }

    protected static string FormatTransactionValues(Money nativeValue, Money foreignValue) =>
        nativeValue == foreignValue ? $"{foreignValue}" : $"{foreignValue} ({nativeValue})";

    public void ValidateClose() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Balance.Amount == 0,
            "Cannot close a non-empty account"
        );

    protected Account() { }
}
