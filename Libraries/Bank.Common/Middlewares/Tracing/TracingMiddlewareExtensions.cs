using Microsoft.AspNetCore.Builder;

namespace Bank.Common.Middlewares.Tracing
{
    public static class TracingMiddlewareExtensions
    {
        public static WebApplication UseTracing(this WebApplication app)
        {
            app.UseMiddleware<TracingMiddleware>();

            return app;
        }
    }
}
