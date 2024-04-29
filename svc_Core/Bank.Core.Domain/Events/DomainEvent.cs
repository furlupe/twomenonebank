namespace Bank.Core.Domain.Events;

public abstract class DomainEvent
{
    public Guid Id { get; protected set; }
    public DateTime ResolvedAt { get; protected set; }
    public Guid IdempotenceKey { get; protected set; }
    public EventState State { get; protected set; }

    public void Fail() => State = EventState.Failed;

    public void Complete() => State = EventState.Completed;

    public void Cancel() => State = EventState.Canceled;

    public DomainEvent(DateTime now, Guid idempotenceKey, EventState state = EventState.Completed)
    {
        ResolvedAt = now;
        State = state;
        IdempotenceKey = idempotenceKey;
    }

    public enum EventState
    {
        Completed,
        Canceled,
        Failed
    }

    protected DomainEvent() { }
}
