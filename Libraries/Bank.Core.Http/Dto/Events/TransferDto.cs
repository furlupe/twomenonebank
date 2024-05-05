using Bank.Core.Domain.Events;

namespace Bank.Core.Http.Dto.Events;

public class TransferDto
{
    public BalanceChangeDto Source { get; protected set; } = null!;
    public BalanceChangeDto Target { get; protected set; } = null!;

    public static TransferDto? From(Transfer? model) =>
        model == null
            ? null
            : new()
            {
                Source = BalanceChangeDto.From(model.Source)!,
                Target = BalanceChangeDto.From(model.Target)!
            };
}
