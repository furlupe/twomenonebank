using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public abstract class BalanceChange(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account target,
    ICurrencyConverter converter
) : Transaction(value, now, idempotenceKey)
{
    public Account Target { get; protected init; } = target;
    protected ICurrencyConverter _converter = converter;

    public override async Task<TransactionEvent> Perform()
    {
        var @event = await PerformTransient();
        Target.AddTransaction(@event);
        return @event;
    }

    protected async Task<Money> GetNativeValue(Money transactionValue) =>
        transactionValue.Currency == Target.Balance.Currency
            ? (transactionValue with { })
            : await _converter.Convert(transactionValue, Target.Balance.Currency);

    protected static string FormatValues(Money nativeValue, Money foreignValue) =>
        nativeValue == foreignValue ? $"{foreignValue}" : $"{foreignValue} ({nativeValue})";

    protected void ValidateTargetOpen() =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            !Target.IsClosed,
            "Target account is closed."
        );
}
