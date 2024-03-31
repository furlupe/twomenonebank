using Bank.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Bank.Auth.Http;
using Bank.Auth.Http.TokenClient;

namespace Bank.Core.Http.Client
{
    public static class RegisterCoreClientExtension
    {
        public static WebApplicationBuilder AddCoreClient(this WebApplicationBuilder builder)
        {
            builder.BindOptions<CoreClientOptions>();
            builder.AddAuthTokenClient();

            builder.Services.AddHttpClient<CoreClient>()
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, sslErrs) => true
                })
                .AddHttpMessageHandler<AuthorizationHandler>();

            return builder;
        }
    }
}
