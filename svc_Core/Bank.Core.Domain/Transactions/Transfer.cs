namespace Bank.Core.Domain.Transactions;

public class Transfer : Transaction
{
    public Account Target { get; set; }
}
