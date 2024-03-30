using Bank.Common.Money;

namespace Bank.Core.Common;

public class Transaction
{
    public Money Value { get; set; }
    public Guid InitiatorId { get; set; }
    public Guid SourceAccountId { get; set; }
    public TransactionType Type { get; set; }
    public BalanceChange? BalanceChange { get; set; }
    public Transfer? Transfer { get; set; }

    public enum TransactionType
    {
        BalanceChange,
        Transfer,
    }
}

public class Transfer
{
    public Guid TargetUserId { get; set; }
    public TransferType Type { get; set; }

    public enum TransferType
    {
        me2me,
        p2p
    }
}

public class BalanceChange
{
    public BalanceChangeType Type { get; set; }
    public CreditPayment? CreditPayment { get; set; }

    public enum BalanceChangeType
    {
        Deposit,
        Withdrawal,
        CreditPayment,
    }
}

public class CreditPayment
{
    public Guid CreditId { get; set; }
}
