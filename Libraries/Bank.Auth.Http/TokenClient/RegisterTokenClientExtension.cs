using Bank.Common.Extensions;
using Bank.Common.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Auth.Http.TokenClient
{
    public static class RegisterTokenClientExtension
    {
        public static WebApplicationBuilder AddAuthTokenClient(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            builder.RegisterHandlers();

            services.AddHttpContextAccessor();

            services.AddSingleton<TokenCache>();
            services
                .AddHttpClient<AuthTokenClient>()
                .AddResilience()
                .AddHttpMessageHandler<TracingHandler>();

            return builder;
        }
    }
}
