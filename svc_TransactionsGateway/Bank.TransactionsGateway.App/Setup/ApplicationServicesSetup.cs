using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Exceptions.WebApiException;
using Bank.TransactionsGateway.App.Services;

namespace Bank.TransactionsGateway.App.Setup;

public static class ApplicationServicesSetup
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.AddInfrastrucureServices();

        var services = builder.Services;
        services.AddScoped<ITransactionService, TransactionService>();

        return builder;
    }

    private static WebApplicationBuilder AddInfrastrucureServices(
        this WebApplicationBuilder builder
    )
    {
        builder.AddConfiguration();

        var services = builder.Services;
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        Admonish.Validator.UnsafeConfigureException(x => new ValidationException(x.ToDictionary()));
        return builder;
    }

    public static async Task UseApplicationServices(this WebApplication app) { }
}
