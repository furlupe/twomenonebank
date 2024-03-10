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
    public List<AccountEvent> Events { get; protected set; } = [];

    public void Deposit(Deposit transaction, DateTime now)
    {
        Balance += transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Deposited {transaction.Value}",
                AccountEvent.Type.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    transaction.Value,
                    BalanceChange.Type.Deposit
                )
            )
        );
    }

    public void Withdraw(Withdraw transaction, DateTime now)
    {
        Balance -= transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Deposited {transaction.Value}",
                AccountEvent.Type.BalanceChange,
                now,
                balanceChange: new BalanceChange(
                    this,
                    transaction.Value,
                    BalanceChange.Type.Withdrawal
                )
            )
        );
    }

    public void RepayCredit(RepayCredit transaction, DateTime now)
    {
        Balance -= transaction.Value;
        Events.Add(
            new AccountEvent(
                $"Deposited {transaction.Value}",
                AccountEvent.Type.BalanceChange,
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

    public Account(User user)
    {
        User = user;
    }

    protected Account() { }
}
