namespace Bank.Core.Domain.Transactions;

public class RepayCredit : Withdraw
{
    public Guid CreditId { get; set; }
}
