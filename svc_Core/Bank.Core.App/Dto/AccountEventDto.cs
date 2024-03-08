using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto;

public class AccountEventDto
{
    public Guid Id { get; set; }
    public DateTime ResolvedAt { get; protected set; }

    public long Value { get; set; }
    public string Comment { get; set; }

    public EventType Type { get; set; }

    public enum EventType
    {
        Transfer,
        Withdrawal,
        Deposit,
        CreditPayment
    }

    public AccountEventDto From(BalanceChange source)
    {
        return new AccountEventDto
        {
            Id = source.Id,
            ResolvedAt = source.ResolvedAt,
            Value = source.Value,
            Type = source switch
            {
                CreditPayment => EventType.CreditPayment,
                Withdrawal => EventType.Withdrawal,
                Deposit => EventType.Deposit,
                _ => throw new ArgumentOutOfRangeException(),
            },
            Comment = source.Comment,
        };
    }

    public AccountEventDto From(Transfer source)
    {
        return new AccountEventDto
        {
            Type = EventType.Transfer,
            Comment = source.Comment,
            Value = source.Value,
            ResolvedAt = source.ResolvedAt,
            Id = source.Id,
        };
    }
}
