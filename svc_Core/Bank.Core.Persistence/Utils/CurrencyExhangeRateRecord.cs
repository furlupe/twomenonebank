using Bank.Common.Money;

namespace Bank.Core.Persistence.Utils;

public record class CurrencyExhangeRateRecord(
    Currency Source,
    Currency Target,
    decimal Value,
    DateTime ExpiresAt
)
{
    public Currency Source { get; init; } = Source;
    public Currency Target { get; init; } = Target;
    public decimal Value { get; init; } = Value;
    public DateTime ExpiresAt { get; init; } = ExpiresAt;
}
