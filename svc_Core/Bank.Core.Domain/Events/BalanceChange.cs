using Bank.Common.Money;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Domain.Events;

[Owned]
public class BalanceChange
{
    public Money NativeValue { get; protected set; }
    public Money ForeignValue { get; protected set; }
    public Account Account { get; protected set; }
    public Guid AccountId { get; protected set; }

    public BalanceChangeType EventType { get; protected set; }

    public BalanceChange(
        Account account,
        Money nativeValue,
        Money foreignValue,
        BalanceChangeType type
    )
    {
        Account = account;
        NativeValue = nativeValue;
        ForeignValue = foreignValue;
        EventType = type;
    }

    protected BalanceChange() { }
}

public enum BalanceChangeType
{
    Withdrawal,
    Deposit,
}
