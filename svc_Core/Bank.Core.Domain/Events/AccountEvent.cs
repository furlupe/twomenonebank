namespace Bank.Core.Domain.Events;

public class AccountEvent : DomainEvent
{
    public string Comment { get; protected set; }
    public AccountEventType EventType { get; protected set; }
    public BalanceChange? BalanceChange { get; protected set; }
    public Transfer? Transfer { get; protected set; }

    public AccountEvent(
        string comment,
        AccountEventType eventType,
        DateTime now,
        EventState state = EventState.Completed,
        BalanceChange? balanceChange = null,
        Transfer? transfer = null
    )
        : base(now, state)
    {
        Comment = comment;
        EventType = eventType;
        BalanceChange = balanceChange;
        Transfer = transfer;
    }

    protected AccountEvent() { }
}

public enum AccountEventType
{
    BalanceChange,
    Transfer,
}
