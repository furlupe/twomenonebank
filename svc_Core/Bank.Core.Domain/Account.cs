using System.Security.Principal;
using Bank.Attributes.Attributes;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;

namespace Bank.Core.Domain;

[ModelName("Account")]
public class Account : StoredModel
{
    public string Name { get; set; }
    public long Balance { get; set; } = 0;
    public User User { get; set; }
    public Guid UserId { get; set; }
    public List<AccountEvent> Events { get; protected set; } = [];

    public void Deposit(Deposit transaction, DateTime now)
    {
        Balance += transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Deposited {transaction.Value}.",
                AccountEventType.BalanceChange,
                now,
                balanceChange: new BalanceChange(this, transaction.Value, BalanceChangeType.Deposit)
            )
        );
    }

    public void Withdraw(Withdraw transaction, DateTime now)
    {
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            transaction.Value <= Balance,
            "Withdraw sum exceeds account balance."
        );

        Balance -= transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Withdrew {transaction.Value}.",
                AccountEventType.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    transaction.Value,
                    BalanceChangeType.Withdrawal
                )
            )
        );
    }

    public void RepayCredit(RepayCredit transaction, DateTime now)
    {
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            transaction.Value <= Balance,
            "Payment sum exceeds account balance."
        );

        Balance -= transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Payed {transaction.Value} for credit.",
                AccountEventType.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    transaction.Value,
                    new CreditPayment(transaction.CreditId)
                )
            )
        );
    }

    public void ValidateClose() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Balance == 0,
            "Cannot close a non-empty account"
        );

    public Account(User user, string name)
    {
        User = user;
        Name = name;
    }

    protected Account() { }
}
