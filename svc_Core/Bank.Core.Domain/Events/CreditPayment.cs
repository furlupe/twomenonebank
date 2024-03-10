namespace Bank.Core.Domain.Events;

public class CreditPayment
{
    public Guid CreditId { get; protected set; }

    public CreditPayment(Guid creditId)
    {
        CreditId = creditId;
    }
}
