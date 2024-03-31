using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Domain.Events;

[Owned]
public class CreditTransfer
{
    public CreditTransferType Type { get; protected set; }
    public Guid CreditId { get; protected set; }

    public CreditTransfer(BalanceChange source, BalanceChange target, Guid creditId)
    {
        CreditId = creditId;
        Type =
            source.Account.UserId == Guid.Empty
                ? CreditTransferType.CreditIssued
                : CreditTransferType.CreditRepayed;
    }

    protected CreditTransfer() { }
}

public enum CreditTransferType
{
    CreditIssued,
    CreditRepayed
}
