using Bank.Common.Cache;

namespace Bank.Common.Money.Cache;

public interface ICurrencyConversionRatesCache
    : IBackedCache<
        Currency,
        Dictionary<Currency, decimal>,
        ICurrencyConversionRatesCacheBackingStore
    > { }

public interface ICurrencyConversionRatesCacheBackingStore
    : BackedCache<
        Currency,
        Dictionary<Currency, decimal>,
        ICurrencyConversionRatesCacheBackingStore
    >.ICacheBackingStore { }

public interface ICurrencyConversionRatesCacheBackingStoreFactory
    : BackedCache<
        Currency,
        Dictionary<Currency, decimal>,
        ICurrencyConversionRatesCacheBackingStore
    >.ICacheBackingStoreFactory { }
