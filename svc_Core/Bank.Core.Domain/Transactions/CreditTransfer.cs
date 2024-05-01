using Bank.Common.Money;
using Bank.Common.Money.Converter;
using Bank.Core.Domain.Events;

namespace Bank.Core.Domain.Transactions;

public abstract class CreditTransfer(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account source,
    Account target,
    Guid creditId,
    ICurrencyConverter converter,
    string? message = null
) : Transfer(value, now, idempotenceKey, source, target, converter, message)
{
    public Guid CreditId { get; protected init; } = creditId;

    internal override async Task<TransactionEvent> PerformTransient()
    {
        var @event = await base.PerformTransient();
        Events.BalanceChange source = @event.Transfer!.Source,
            target = @event.Transfer!.Target;

        return new TransactionEvent(
            Message,
            Now,
            IdempotenceKey,
            transfer: new Events.Transfer(source, target, new(source, target, CreditId)) { }
        );
    }
}

public class CreditPayment(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account source,
    Account master,
    Guid creditId,
    ICurrencyConverter converter
)
    : CreditTransfer(
        value,
        now,
        idempotenceKey,
        source,
        master,
        creditId,
        converter,
        "Payed for credit."
    ) { }

public class CreditIssuance(
    Money value,
    DateTime now,
    Guid idempotenceKey,
    Account source,
    Account master,
    Guid creditId,
    ICurrencyConverter converter
)
    : CreditTransfer(
        value,
        now,
        idempotenceKey,
        master,
        source,
        creditId,
        converter,
        "Received credit."
    ) { }
