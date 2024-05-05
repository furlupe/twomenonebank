using Bank.Common.Money;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public abstract class Transaction(Money value, DateTime now, Guid idempotenceKey)
{
    public Money Value { get; protected init; } = value;
    public DateTime Now { get; protected init; } = now;
    public Guid IdempotenceKey { get; protected init; } = idempotenceKey;
    public abstract Task<TransactionEvent> Perform();
    internal abstract Task<TransactionEvent> PerformTransient();

    protected void ValidateValue() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Value.Amount > 0,
            "Balance change value must be positive."
        );
}
