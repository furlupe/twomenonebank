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
        DateTime now,
        EventState state = EventState.Completed,
        BalanceChange? balanceChange = null,
        Transfer? transfer = null
    )
    {
        Comment = comment;
        EventType = eventType;
        BalanceChange = balanceChange;
        Transfer = transfer;
        ResolvedAt = now;
        State = state;
    }

    public enum Type
    {
        BalanceChange,
        Transfer,
    }

    protected AccountEvent() { }
}
