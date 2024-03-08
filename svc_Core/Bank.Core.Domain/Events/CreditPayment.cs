namespace Bank.Core.Domain.Events;

public class CreditPayment : Withdrawal
{
    public Guid CreditId { get; set; }
}
