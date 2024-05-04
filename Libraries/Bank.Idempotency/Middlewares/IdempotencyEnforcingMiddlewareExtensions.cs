using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Idempotency.Middlewares
{
    public static class IdempotencyEnforcingMiddlewareExtensions
    {
        public static WebApplicationBuilder AddIdempotency<T>(this WebApplicationBuilder builder) where T : class, IActionService
        {
            builder.Services.AddScoped<IActionService, T>();

            return builder;
        }
        public static WebApplication UseIdempotency(this WebApplication app, params string[] excludePaths)
        {
            app.UseWhen(
                context => { 
                    return !(excludePaths.Contains(context.Request.Path.ToString()) || context.Request.Path.StartsWithSegments("/swagger")); 
                },
                builder => builder.UseMiddleware<IdempotencyEnforcingMiddleware>()
            );
            return app;
        }
    }
}
