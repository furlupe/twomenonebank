using Bank.Attributes.Attributes;
using Bank.Attributes.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Common.Extensions;

public static class OptionsExtensions
{
    public static WebApplicationBuilder BindOptions<T>(this WebApplicationBuilder builder)
        where T : class
    {
        builder.Services.Configure<T>(
            builder.Configuration.GetSection(
                typeof(T).GetAttribute<ConfigurationModelAttribute>().SectionKey
            )
        );
        return builder;
    }

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
