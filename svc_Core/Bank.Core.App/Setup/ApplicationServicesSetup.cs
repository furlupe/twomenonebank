﻿using Bank.Common.DateTimeProvider;
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
        builder.AddLocalConfiguration();

        var services = builder.Services;
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddCoreDbContext(builder.GetConfigurationValue<Configuration>());

        Admonish.Validator.UnsafeConfigureException(x => new ValidationException(x.ToDictionary()));
        return builder;
    }

    private static WebApplicationBuilder AddLocalConfiguration(this WebApplicationBuilder builder)
    {
        bool isDevelopment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            == Environments.Development;

        if (isDevelopment)
        {
            builder.Configuration.AddJsonFile(
                "appsettings.local.json",
                optional: true,
                reloadOnChange: true
            );
        }

        return builder;
    }

    public static async Task UseApplicationServices(this WebApplication app)
    {
        await app.UseCoreDbContext();
    }
}