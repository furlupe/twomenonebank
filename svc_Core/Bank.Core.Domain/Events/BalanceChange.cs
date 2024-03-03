namespace Bank.Core.Domain.Events;

public abstract class BalanceChange : DomainEvent
{
    public long Value { get; protected set; }
    public string Comment { get; protected set; }
    public Account Account { get; protected set; }
}
