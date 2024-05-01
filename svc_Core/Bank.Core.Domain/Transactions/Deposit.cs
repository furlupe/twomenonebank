using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class Deposit(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account target,
    ICurrencyConverter converter
) : BalanceChange(value, now, idempotenceKey, target, converter)
{
    internal override async Task<TransactionEvent> PerformTransient()
    {
        ValidateTargetOpen();
        ValidateValue();
        var nativeValue = await GetNativeValue(Value);
        Target.Balance += nativeValue;

        return new(
            $"Deposited {FormatValues(nativeValue, Value)}.",
            Now,
            IdempotenceKey,
            balanceChange: new Events.BalanceChange(
                Target,
                nativeValue,
                Value,
                BalanceChangeType.Deposit
            )
        );
    }
}
