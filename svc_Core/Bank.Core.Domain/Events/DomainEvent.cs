namespace Bank.Core.Domain.Events;

public abstract class DomainEvent
{
    public Guid Id { get; protected set; }
    public DateTime ResolvedAt { get; protected set; }
    public EventState State { get; protected set; }

    public void Fail() => State = EventState.Failed;

    public void Complete() => State = EventState.Completed;

    public void Cancel() => State = EventState.Canceled;

    public enum EventState
    {
        Completed,
        Canceled,
        Failed
    }
}
