namespace Bank.Core.Domain.Events;

public class Transfer : BalanceChange
{
    public Withdrawal Withdrawal { get; set; }
    public Deposit Deposit { get; set; }
}
