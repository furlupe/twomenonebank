using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.Common.Extensions;

public static class EnableXMLCommentsExtension
{
    public static SwaggerGenOptions UseXmlComments(
        this SwaggerGenOptions options,
        Assembly assembly
    )
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        options.IncludeXmlComments(xmlPath, true);

        return options;
    }
}
