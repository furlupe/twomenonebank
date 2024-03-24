using Bank.Common.Cache;
using Bank.Common.Money;
using Bank.Common.Money.Cache;
using Bank.Core.Persistence.Utils;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.Persistence;

public partial class CoreDbContext : ICurrencyConversionRatesCacheBackingStore
{
    public DbSet<CurrencyExhangeRateRecord> CurrencyExhangeRates { get; set; }

    public async Task AddOrUpdateAsync(
        Currency key,
        Cache<Currency, Dictionary<Currency, decimal>>.Entry entry
    )
    {
        var source = key;
        var targets = entry.Value.Keys;

        await CurrencyExhangeRates
            .Where(rate => rate.Source == source && targets.Contains(rate.Target))
            .ExecuteDeleteAsync();

        foreach (var rate in entry.Value)
            CurrencyExhangeRates.Add(new(key, rate.Key, rate.Value, entry.ExpiresAt));

        await SaveChangesAsync();
    }

    public async Task ClearAsync()
    {
        await CurrencyExhangeRates.ExecuteDeleteAsync();
    }

    public async Task<Cache<Currency, Dictionary<Currency, decimal>>.Entry?> TryGetAsync(
        Currency key
    )
    {
        var rates = await CurrencyExhangeRates
            .Where(x => x.Source == key && x.ExpiresAt > _dateTimeProvider.UtcNow)
            .ToListAsync();

        return rates.Count > 0
            ? new(
                rates.ToDictionary(x => x.Target, x => x.Value),
                rates.Select(x => x.ExpiresAt).Order().First()
            )
            : null;
    }
}
