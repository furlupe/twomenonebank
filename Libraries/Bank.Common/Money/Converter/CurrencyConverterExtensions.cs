using Bank.Common.Extensions;
using Bank.Common.Money.Cache;
using Bank.Common.Money.Source;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Money.Converter;

public static class CurrencyConverterExtensions
{
    /// <summary>
    /// Registers services needed for <see cref="CurrencyConverter"/> to operate in a DI container.
    /// Warning: does not register an implementation for <see cref="ICurrencyConversionRatesCacheBackingStore"/>.
    /// </summary>
    public static WebApplicationBuilder AddCurrencyConverter(this WebApplicationBuilder builder)
    {
        var services = builder.Services;

        builder.BindOptions<CurrencyConversionRatesSourceConfiguration>();

        services
            .AddSingleton<
                ICurrencyConversionRatesCacheBackingStoreFactory,
                CurrencyConversionRatesCacheBackingStoreFactory
            >()
            .AddSingleton<ICurrencyConversionRatesCache, CurrencyConversionRatesCache>()
            .AddScoped<ICurrencyConversionRatesSource, CurrencyConversionRatesSource>()
            .AddHttpClient<CurrencyConversionRatesSource>();

        services.AddScoped<ICurrencyConverter, CurrencyConverter>();

        return builder;
    }
}
