using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class Deposit(Money value, DateTime now, Account target, ICurrencyConverter converter)
    : BalanceChange(value, now, target, converter)
{
    internal override async Task<AccountEvent> PerformTransient()
    {
        ValidateValue();
        var nativeValue = await GetNativeValue(Value);
        Target.Balance += nativeValue;

        return new(
            $"Deposited {FormatValues(nativeValue, Value)}.",
            AccountEventType.BalanceChange,
            Now,
            balanceChange: new Events.BalanceChange(
                Target,
                nativeValue,
                Value,
                BalanceChangeType.Deposit
            )
        );
    }
}
