using Bank.Amqp;
using Bank.Common;
using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Common.Middlewares.Tracing;
using Bank.Common.Money.Cache;
using Bank.Common.Money.Converter;
using Bank.Core.App.Services;
using Bank.Core.App.Services.Amqp;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Persistence;
using Bank.Exceptions.WebApiException;
using Bank.Logging.Extensions;
using MassTransit;

namespace Bank.Core.App.Setup;

public static class ApplicationServicesSetup
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.AddInfrastrucureServices();

        var services = builder.Services;

        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<ITransactionsService, TransactionsService>()
            .AddScoped<ITransactionsFactory, TransactionsFactory>();

        services.AddScoped<ICurrencyConversionRatesCacheBackingStore, CoreDbContext>();
        builder.AddCurrencyConverter();

        return builder;
    }

    private static WebApplicationBuilder AddInfrastrucureServices(
        this WebApplicationBuilder builder
    )
    {
        builder.AddConfiguration();
        builder.BindFeaturesOptions();
        builder.AddMassTransit(configure: cfg =>
        {
            cfg.AddConsumer<TransactionsConsumer>();
        });
        builder.AddLogging();

        var services = builder.Services;
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddCoreDbContext(builder.GetConfigurationValue<Configuration>());

        Admonish.Validator.UnsafeConfigureException(x => new ValidationException(x.ToDictionary()));
        return builder;
    }

    public static async Task UseApplicationServices(this WebApplication app)
    {
        await app.UseCoreDbContext();
        app.UseLogging();
        app.UseTracing();
    }
}
