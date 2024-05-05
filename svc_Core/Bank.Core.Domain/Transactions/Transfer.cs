using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class Transfer(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account source,
    Account target,
    ICurrencyConverter converter,
    string? message = null
) : Transaction(value, now, idempotenceKey)
{
    public Account Source { get; protected init; } = source;
    public Account Target { get; protected init; } = target;
    public string Message { get; protected init; } = message ?? "Transferred.";

    public override async Task<TransactionEvent> Perform()
    {
        var @event = await PerformTransient();
        Source.AddTransaction(@event);
        Target.AddTransaction(@event);
        return @event;
    }

    internal override async Task<TransactionEvent> PerformTransient()
    {
        ValidateCircuit();
        var withdrawal = await new Withdrawal(
            Value,
            Now,
            IdempotenceKey,
            Source,
            converter
        ).PerformTransient()!;
        var deposit = await new Deposit(
            Value,
            Now,
            IdempotenceKey,
            Target,
            converter
        ).PerformTransient()!;
        ValidateEquality(
            withdrawal.BalanceChange!.ForeignValue,
            deposit.BalanceChange!.ForeignValue
        );

        return new TransactionEvent(
            Message,
            Now,
            IdempotenceKey,
            transfer: new Events.Transfer(withdrawal.BalanceChange, deposit.BalanceChange) { }
        );
    }

    protected void ValidateCircuit() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            Source.Id != Target.Id,
            "Source and target can't be the same account."
        );

    protected void ValidateEquality(Money withdrawalValue, Money depositValue) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            withdrawalValue == depositValue,
            "Withdrawal and deposit values must be equal."
        );
}
