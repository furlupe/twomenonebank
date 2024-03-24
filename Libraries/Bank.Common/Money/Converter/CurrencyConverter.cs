using Bank.Common.DateTimeProvider;
using Bank.Common.Money.Cache;
using Bank.Common.Money.Source;
using Bank.Exceptions.WebApiException;

namespace Bank.Common.Money.Converter;

public class CurrencyConverter(
    ICurrencyConversionRatesCache rateCache,
    ICurrencyConversionRatesSource rateSource,
    IDateTimeProvider dateProvider
) : ICurrencyConverter
{
    public async Task<Money> Convert(Money value, Currency target)
    {
        var rate = await GetConversionRate(value.Currency, target);
        return new(value.Amount * rate, target);
    }

    public async Task<decimal> GetConversionRate(Currency source, Currency target)
    {
        if (source == target)
            return 1;

        decimal rate;
        var sourceRates = await rateCache.TryGetAsync(source, dateProvider.UtcNow);
        if (sourceRates is not null)
        {
            if (sourceRates.TryGetValue(target, out rate))
                return rate;

            var targetRates = await rateCache.TryGetAsync(target, dateProvider.UtcNow);
            if (targetRates is not null && targetRates.Keys.Intersect(sourceRates.Keys).Any())
            {
                var commonTarget = targetRates.Keys.Intersect(sourceRates.Keys).First();
                return sourceRates[commonTarget] / targetRates[commonTarget];
            }
        }

        sourceRates = await rateSource.GetConversionRates(source);
        if (!sourceRates.TryGetValue(target, out rate))
            throw new ImATeapotException($"Could not retrieve conversion rates for {source}.");

        var expiresAt = dateProvider.UtcNow.AddDays(1);
        List<Task> updates = [rateCache.AddOrUpdateAsync(source, sourceRates, expiresAt)];
        updates.AddRange(
            sourceRates.Select(x =>
                rateCache.AddOrUpdateAsync(x.Key, new() { { source, 1 / x.Value } }, expiresAt)
            )
        );

        await Task.WhenAll(updates);

        return rate;
    }
}
