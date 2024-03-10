using Bank.Common.Constants;
using Bank.Common.DateTimeProvider;
using Bank.Common.Extensions;
using Bank.Core.App.Services;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Persistence;
using Bank.Exceptions.WebApiException;

namespace Bank.Core.App.Setup;

public static class ApplicationServicesSetup
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.AddInfrastrucureServices();

        var services = builder.Services;
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();

        return builder;
    }

    private static WebApplicationBuilder AddInfrastrucureServices(
        this WebApplicationBuilder builder
    )
    {
        builder.AddConfiguration();

        var services = builder.Services;
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        services.AddCoreDbContext(builder.GetConfigurationValue<Configuration>());

        Admonish.Validator.UnsafeConfigureException(x => new ValidationException(x.ToDictionary()));
        return builder;
    }

    private static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        switch (environment)
        {
            case Common.Constants.Environments.Development:
            {
                builder.AddLocalConfiguration();
                break;
            }
            case Common.Constants.Environments.Staging:
            {
                builder.AddStagingConfiguration();
                break;
            }
        }

        return builder;
    }

    private static void AddLocalConfiguration(this WebApplicationBuilder builder) =>
        builder.Configuration.AddJsonFile(
            "appsettings.Local.json",
            optional: true,
            reloadOnChange: true
        );

    private static void AddStagingConfiguration(this WebApplicationBuilder builder) =>
        builder.Configuration.AddJsonFile(
            "appsettings.Staging.json",
            optional: false,
            reloadOnChange: true
        );

    public static async Task UseApplicationServices(this WebApplication app)
    {
        await app.UseCoreDbContext();
    }
}
