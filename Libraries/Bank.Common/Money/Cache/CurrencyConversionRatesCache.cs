using Bank.Common.Cache;

namespace Bank.Common.Money.Cache;

public class CurrencyConversionRatesCache(
    ICurrencyConversionRatesCacheBackingStoreFactory storeFactory
)
    : BackedCache<
        Currency,
        Dictionary<Currency, decimal>,
        ICurrencyConversionRatesCacheBackingStore
    >(storeFactory),
        ICurrencyConversionRatesCache { }

public class CurrencyConversionRatesCacheBackingStoreFactory(IServiceProvider serviceProvider)
    : BackedCache<
        Currency,
        Dictionary<Currency, decimal>,
        ICurrencyConversionRatesCacheBackingStore
    >.CacheBackingStoreFactory(serviceProvider),
        ICurrencyConversionRatesCacheBackingStoreFactory { }
