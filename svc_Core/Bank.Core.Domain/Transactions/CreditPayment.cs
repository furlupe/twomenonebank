using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Common.Utils;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public class CreditPayment(
    Money value,
    DateTime now,
    Account target,
    Guid creditId,
    ICurrencyConverter converter
) : Withdrawal(value, now, target, converter)
{
    public Guid CreditId { get; protected init; } = creditId;

    internal override async Task<AccountEvent> PerformTransient()
    {
        var nativeValue = await GetNativeValue(Value);
        Validation.Check(
            ExceptionConstants.MsgInvalidValue,
            nativeValue <= Target.Balance,
            "Payment sum exceeds account balance."
        );
        Target.Balance += nativeValue;

        return new(
            $"Payed {FormatValues(nativeValue, Value)} for credit.",
            AccountEventType.BalanceChange,
            Now,
            balanceChange: new Events.BalanceChange(
                Target,
                nativeValue,
                Value,
                new Events.CreditPayment(CreditId)
            )
        );
    }
}
