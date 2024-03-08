using Bank.Attributes.Attributes;
using Bank.Attributes.Utils;
using Bank.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Extensions;

public static class ConfigurationExtensions
{
    public static T GetConfigurationValue<T>(this WebApplicationBuilder builder) =>
        builder.GetConfigurationValue<T>(
            typeof(T).GetAttribute<ConfigurationModelAttribute>().SectionKey
        );

    public static T GetConfigurationValue<T>(this WebApplicationBuilder builder, string key) =>
        builder.Configuration.GetSection(key).Get<T>()
        ?? throw new ConfigurationException(
            $"Configuration failed: could not get value for {typeof(T).Name} by section key '{key}'"
        );
}
