using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class Transfer(
    Money value,
    DateTime now,
    Account source,
    Account target,
    ICurrencyConverter converter
) : Transaction(value, now)
{
    public Account Source { get; protected init; } = source;
    public Account Target { get; protected init; } = target;

    public override async Task Perform()
    {
        var @event = await PerformTransient();
        Source.AddEvent(@event);
        Target.AddEvent(@event);
    }

    internal override async Task<AccountEvent> PerformTransient()
    {
        ValidateCircuit();
        var withdrawal = await new Withdrawal(Value, Now, Source, converter).PerformTransient()!;
        var deposit = await new Deposit(Value, Now, Source, converter).PerformTransient()!;
        ValidateEquality(
            withdrawal.BalanceChange!.ForeignValue,
            deposit.BalanceChange!.ForeignValue
        );

        return new AccountEvent(
            $"Transferred.",
            AccountEventType.Transfer,
            Now,
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
