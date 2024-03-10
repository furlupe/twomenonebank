using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto.Events;

public class TransferDto
{
    public Guid Id { get; set; }
    public BalanceChangeDto Source { get; protected set; }
    public BalanceChangeDto Target { get; protected set; }

    public static TransferDto? From(Transfer? model) =>
        model == null
            ? null
            : new()
            {
                Id = model.Id,
                Source = BalanceChangeDto.From(model.Source)!,
                Target = BalanceChangeDto.From(model.Target)!
            };
}
