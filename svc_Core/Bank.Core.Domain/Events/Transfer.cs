namespace Bank.Core.Domain.Events;

public class Transfer : DomainEvent
{
    public BalanceChange Source { get; protected set; }
    public BalanceChange Target { get; protected set; }

    public Transfer(BalanceChange source, BalanceChange target)
    {
        Source = source;
        Target = target;
    }

    protected Transfer() { }
}
