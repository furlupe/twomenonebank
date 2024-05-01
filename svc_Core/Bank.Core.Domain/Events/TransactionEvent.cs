namespace Bank.Core.Domain.Events;

public class TransactionEvent : DomainEvent
{
    public string Comment { get; protected set; } = null!;
    public TransactionEventType EventType { get; protected set; }
    public BalanceChange? BalanceChange { get; protected set; }
    public Transfer? Transfer { get; protected set; }

    public TransactionEvent(
        string comment,
        DateTime now,
        Guid idempotenceKey,
        BalanceChange balanceChange,
        EventState state = EventState.Completed
    )
        : base(now, idempotenceKey, state)
    {
        Comment = comment;
        EventType = TransactionEventType.BalanceChange;
        BalanceChange = balanceChange;
    }

    public TransactionEvent(
        string comment,
        DateTime now,
        Guid idempotenceKey,
        Transfer transfer,
        EventState state = EventState.Completed
    )
        : base(now, idempotenceKey, state)
    {
        Comment = comment;
        EventType = TransactionEventType.Transfer;
        Transfer = transfer;
    }

    protected TransactionEvent() { }
}

public enum TransactionEventType
{
    BalanceChange,
    Transfer,
}
