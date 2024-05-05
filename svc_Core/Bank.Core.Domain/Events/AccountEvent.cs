namespace Bank.Core.Domain.Events;

public class AccountEvent : DomainEvent
{
    public AccountEventType EventType { get; protected set; }

    public AccountEvent(
        DateTime now,
        Guid idempotenceKey,
        AccountEventType type,
        EventState state = EventState.Completed
    )
        : base(now, idempotenceKey, state)
    {
        EventType = type;
    }

    protected AccountEvent() { }
}

public enum AccountEventType
{
    Open,
    Close
}
