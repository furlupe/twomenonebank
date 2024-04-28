using Bank.Attributes.Attributes;
using Bank.Common.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Bank.Common;

[ConfigurationModel("Features")]
public class FeaturesOptions
{
    public bool TransientErrors { get; set; }
}

public static class FeaturesExtensions
{
    public static WebApplicationBuilder BindFeaturesOptions(this WebApplicationBuilder builder) =>
        builder.BindOptions<FeaturesOptions>();

    public static bool TransientErrorsEnabled(this WebApplication app) =>
        app.Configuration.GetConfigurationValue<FeaturesOptions>().TransientErrors;
}
