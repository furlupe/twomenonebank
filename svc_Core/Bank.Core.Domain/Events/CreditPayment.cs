namespace Bank.Core.Domain.Events;

public class CreditPayment(Guid creditId)
{
    public Guid CreditId { get; protected set; } = creditId;
}
