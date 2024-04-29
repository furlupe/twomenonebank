using System.Text.Json.Serialization;
using Bank.Common.Money;

namespace Bank.Core.Common;

public class Transaction
{
    public Money Value { get; set; } = null!;
    public Guid InitiatorId { get; set; }
    public Guid IdempotenceKey { get; set; }
    public Guid SourceId { get; set; }
    public TransactionType Type { get; set; }
    public BalanceChange? BalanceChange { get; set; }
    public Transfer? Transfer { get; set; }

    public Transaction(
        Money value,
        Guid initiatorId,
        Guid idempotenceKey,
        Guid sourceId,
        BalanceChange? balanceChange
    )
    {
        Value = value;
        InitiatorId = initiatorId;
        IdempotenceKey = idempotenceKey;
        SourceId = sourceId;
        Type = TransactionType.BalanceChange;
        BalanceChange = balanceChange;
    }

    public Transaction(
        Money value,
        Guid initiatorId,
        Guid idempotenceKey,
        Guid sourceId,
        Transfer? transfer
    )
    {
        Value = value;
        InitiatorId = initiatorId;
        IdempotenceKey = idempotenceKey;
        SourceId = sourceId;
        Type = TransactionType.Transfer;
        Transfer = transfer;
    }

    [JsonConstructor]
    protected Transaction() { }

    public enum TransactionType
    {
        BalanceChange,
        Transfer,
    }
}

public class BalanceChange
{
    public BalanceChangeType Type { get; set; }

    public enum BalanceChangeType
    {
        Deposit,
        Withdrawal
    }
}

public class Transfer
{
    /// <summary>
    /// Contains either an Id of the user if the transfer is P2P,
    /// or an Id of an account if the transfer is Me2Me or Credit.
    /// In the latter case, (<see cref="Guid.Empty"/>) would mean that this
    /// transfer is a credit payment.
    /// </summary>
    public Guid TargetId { get; set; }
    public TransferType Type { get; set; }
    public CreditTransfer? CreditTransfer { get; set; }
    public string? Message { get; set; }

    public enum TransferType
    {
        Me2Me,
        P2P,
        Credit
    }
}

public class CreditTransfer
{
    public Guid CreditId { get; set; }
}
