namespace Bank.Core.Domain.Events;

public class TransactionEvent : DomainEvent
{
    public string Comment { get; protected set; } = null!;
    public TransactionEventType EventType { get; protected set; }
    public BalanceChange? BalanceChange { get; protected set; }
    public Transfer? Transfer { get; protected set; }

    public Dictionary<Guid, string> GetClientMessages()
    {
        Dictionary<Guid, string> result = [];
        if (
            EventType is TransactionEventType.BalanceChange
            && BalanceChange!.Account.OwnerId != Guid.Empty
        )
        {
            result.Add(
                BalanceChange.Account.OwnerId,
                $"Your account {BalanceChange.Account.Name} has new transactions"
            );
        }

        if (EventType is TransactionEventType.Transfer)
        {
            if (Transfer!.Type is TransferType.Credit) { }
        }

        return result;
    }

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
