using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace Bank.Common.Extensions
{
    public static class WebApplicationExtensions
    {
        private const DynamicallyAccessedMemberTypes MiddlewareAccessibility =
            DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods;

        /// <summary>
        /// Registers middleware in pipeline, but makes it ignore swagger endpoints 
        /// </summary>
        public static WebApplication UseMiddlewareIgnoreSwagger<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this WebApplication app, params object?[] args)
        {
            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/swagger"),
                builder => builder.UseMiddleware<TMiddleware>(args)
            );

            return app;
        }
    }
}
