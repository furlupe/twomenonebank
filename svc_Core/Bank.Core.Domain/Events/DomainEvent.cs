namespace Bank.Core.Domain.Events;

public abstract class DomainEvent
{
    public Guid Id { get; protected set; }
    public DateTime ResolvedAt { get; protected set; }
    public EventState State { get; protected set; }

    public enum EventState
    {
        Completed,
        Canceled,
        Failed
    }
}
