namespace Bank.Core.Domain.Events;

public class AccountEvent : DomainEvent
{
    public string Comment { get; protected set; }

    public Type EventType { get; protected set; }
    public BalanceChange? BalanceChange { get; protected set; }
    public Transfer? Transfer { get; protected set; }

    public AccountEvent(
        string comment,
        Type eventType,
        BalanceChange? balanceChange,
        Transfer? transfer
    )
    {
        Comment = comment;
        EventType = eventType;
        BalanceChange = balanceChange;
        Transfer = transfer;
    }

    public enum Type
    {
        BalanceChange,
        Transfer,
    }

    protected AccountEvent() { }
}
