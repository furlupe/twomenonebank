using Bank.Attributes.Attributes;
using Bank.Attributes.Utils;
using Bank.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Bank.Common.Extensions;

public static class ConfigurationExtensions
{
    public static T GetConfigurationValue<T>(this WebApplicationBuilder builder) =>
        builder.Configuration.GetConfigurationValue<T>();

    public static T GetConfigurationValue<T>(this IConfiguration configuration) =>
        configuration.GetConfigurationValue<T>(
            typeof(T).GetAttribute<ConfigurationModelAttribute>().SectionKey
        );

    public static T GetConfigurationValue<T>(this WebApplicationBuilder builder, string key) =>
        builder.Configuration.GetConfigurationValue<T>(key);

    public static T GetConfigurationValue<T>(this IConfiguration configuration, string key) =>
        configuration.GetSection(key).Get<T>()
        ?? throw new ConfigurationException(
            $"Configuration failed: could not get value for {typeof(T).Name} by section key '{key}'"
        );

    public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        switch (environment)
        {
            case Constants.Environments.Development:
            {
                builder.AddLocalConfiguration();
                break;
            }
            case Constants.Environments.Staging:
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
}
