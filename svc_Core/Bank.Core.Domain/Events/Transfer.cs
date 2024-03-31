using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Domain.Events;

[Owned]
public class Transfer
{
    public BalanceChange Source { get; protected set; }
    public BalanceChange Target { get; protected set; }
    public TransferType Type { get; protected set; }
    public CreditTransfer? CreditTransfer { get; protected set; }

    public Transfer(BalanceChange source, BalanceChange target)
    {
        Source = source;
        Target = target;
        Type = GetTransferType(source, target);
    }

    public Transfer(BalanceChange source, BalanceChange target, CreditTransfer creditTransfer)
    {
        Source = source;
        Target = target;
        Type = TransferType.Credit;
        CreditTransfer = creditTransfer;
    }

    protected TransferType GetTransferType(BalanceChange source, BalanceChange target) =>
        source.Account.UserId == target.Account.UserId ? TransferType.Me2Me : TransferType.P2P;

    protected Transfer() { }
}

public enum TransferType
{
    Me2Me,
    P2P,
    Credit
}
