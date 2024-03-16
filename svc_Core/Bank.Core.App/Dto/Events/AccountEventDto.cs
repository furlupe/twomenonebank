using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto.Events;

public class AccountEventDto
{
    public Guid Id { get; set; }
    public string Comment { get; protected set; }

    public AccountEventType EventType { get; protected set; }
    public BalanceChangeDto? BalanceChange { get; protected set; }
    public TransferDto? Transfer { get; protected set; }
    public DateTime ResolvedAt { get; protected set; }
    public DomainEvent.EventState State { get; protected set; }

    public static AccountEventDto From(AccountEvent model) =>
        new()
        {
            Id = model.Id,
            Comment = model.Comment,
            ResolvedAt = model.ResolvedAt,
            State = model.State,
            EventType = model.EventType,
            BalanceChange = BalanceChangeDto.From(model.BalanceChange),
            Transfer = TransferDto.From(model.Transfer),
        };
}
