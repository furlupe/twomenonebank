namespace Bank.Core.Domain.Transactions;

public class CreditPayment : Withdrawal
{
    public Guid CreditId { get; set; }
}
