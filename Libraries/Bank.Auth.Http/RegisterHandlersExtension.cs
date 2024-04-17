using Bank.Common.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Auth.Http
{
    public static class RegisterHandlersExtension
    {
        public static WebApplicationBuilder RegisterHandlers(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddTransient<TracingHandler>()
                .AddTransient<AuthorizationHandler>();

            return builder;
        }
    }
}
