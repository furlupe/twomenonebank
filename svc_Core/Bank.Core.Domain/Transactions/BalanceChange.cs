using Bank.Common.Money;
using Bank.Common.Money.Converter;

namespace Bank.Core.Domain.Transactions;

public abstract class BalanceChange(
    Money value,
    DateTime now,
    Account target,
    ICurrencyConverter converter
) : Transaction(value, now)
{
    public Account Target { get; protected init; } = target;
    protected ICurrencyConverter _converter = converter;

    public override async Task Perform()
    {
        Target.AddEvent(await PerformTransient());
    }

    protected async Task<Money> GetNativeValue(Money transactionValue) =>
        transactionValue.Currency == Target.Balance.Currency
            ? (transactionValue with { })
            : await _converter.Convert(transactionValue, Target.Balance.Currency);

    protected static string FormatValues(Money nativeValue, Money foreignValue) =>
        nativeValue == foreignValue ? $"{foreignValue}" : $"{foreignValue} ({nativeValue})";
}
