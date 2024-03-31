using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public abstract class CreditTransfer(
    Money value,
    DateTime now,
    Account source,
    Account target,
    Guid creditId,
    ICurrencyConverter converter,
    string? message = null
) : Transfer(value, now, source, target, converter, message)
{
    public Guid CreditId { get; protected init; } = creditId;

    internal override async Task<AccountEvent> PerformTransient()
    {
        var @event = await base.PerformTransient();
        Events.BalanceChange source = @event.Transfer!.Source,
            target = @event.Transfer!.Target;

        return new AccountEvent(
            Message,
            AccountEventType.Transfer,
            Now,
            transfer: new Events.Transfer(source, target, new(source, target, CreditId)) { }
        );
    }
}

public class CreditPayment(
    Money value,
    DateTime now,
    Account source,
    Account master,
    Guid creditId,
    ICurrencyConverter converter
) : CreditTransfer(value, now, source, master, creditId, converter, "Payed for credit.") { }

public class CreditIssuance(
    Money value,
    DateTime now,
    Account source,
    Account master,
    Guid creditId,
    ICurrencyConverter converter
) : CreditTransfer(value, now, source, master, creditId, converter, "Received credit.") { }
