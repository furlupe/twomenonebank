using Bank.Attributes.Attributes;
using Bank.Attributes.Utils;

namespace Bank.Core.App.Setup;

public static class SetupUtils
{
    public static T GetConfiguration<T>(this WebApplicationBuilder builder)
    {
        var attribute = typeof(T).GetAttribute<ConfigurationModelAttribute>();
        return builder.Configuration.GetConfiguration<T>(attribute.SectionKey);
    }

    public static T GetConfiguration<T>(this WebApplicationBuilder builder, string key) =>
        builder.Configuration.GetConfiguration<T>(key);

    public static T GetConfiguration<T>(this IConfiguration configuration, string key) =>
        configuration.GetSection(key).Get<T>()
        ?? throw new Exception($"No such key: {key} was not found in configuration.");

    public static WebApplicationBuilder BindOptions<T>(
        this WebApplicationBuilder builder,
        string sectionKey
    )
        where T : class
    {
        builder.Services.Configure<T>(builder.Configuration.GetSection(sectionKey));
        return builder;
    }
}
