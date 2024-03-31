using Bank.Common.Money;
using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto.Events;

public class BalanceChangeDto
{
    public Money NativeValue { get; protected set; }
    public Money ForeignValue { get; protected set; }
    public Guid AccountId { get; protected set; }

    public BalanceChangeType EventType { get; protected set; }

    public static BalanceChangeDto? From(BalanceChange? model) =>
        model == null
            ? null
            : new()
            {
                AccountId = model.AccountId,
                NativeValue = model.NativeValue,
                ForeignValue = model.ForeignValue,
                EventType = model.EventType,
            };
}
