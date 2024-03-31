using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Auth.Http.TokenClient
{
    public static class RegisterTokenClientExtension
    {
        public static WebApplicationBuilder AddAuthTokenClient(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddSingleton<TokenCache>();
            services.AddScoped<AuthorizationHandler>();
            services.AddHttpClient<AuthTokenClient>();

            return builder;
        }
    }
}
