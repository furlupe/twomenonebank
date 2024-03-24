using Bank.Common.Money;

namespace Bank.Core.Common;

public abstract class Transaction
{
    public Guid TargetId { get; set; }
    public Money Value { get; set; }
}

public class DepositTransaction : Transaction { }

public class WithdrawalTransaction : Transaction { }
