using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class Withdrawal(Money value, DateTime now, Account target, ICurrencyConverter converter)
    : BalanceChange(value, now, target, converter)
{
    internal override async Task<AccountEvent> PerformTransient()
    {
        ValidateValue();
        var nativeValue = await GetNativeValue(Value);
        ValidateWithdrawal(nativeValue);
        Target.Balance += nativeValue;

        return new(
            $"Withdrew {FormatValues(nativeValue, Value)}.",
            AccountEventType.BalanceChange,
            Now,
            balanceChange: new Events.BalanceChange(
                Target,
                nativeValue,
                Value,
                BalanceChangeType.Withdrawal
            )
        );
    }

    protected void ValidateWithdrawal(Money nativeValue) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidValue,
            nativeValue <= Target.Balance,
            "Withdrawal sum can not exceed account balance."
        );
}
