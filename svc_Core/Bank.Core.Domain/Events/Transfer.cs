namespace Bank.Core.Domain.Events;

public class Transfer
{
    public BalanceChange Source { get; protected set; }
    public BalanceChange Target { get; protected set; }

    public TransferType Type { get; protected set; }

    public Transfer(BalanceChange source, BalanceChange target)
    {
        Source = source;
        Target = target;
        Type =
            source.Account.UserId == target.Account.UserId ? TransferType.me2me : TransferType.p2p;
    }

    protected Transfer() { }
}

public enum TransferType
{
    me2me,
    p2p
}
