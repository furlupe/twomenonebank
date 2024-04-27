using Bank.Auth.Http;
using Bank.Auth.Http.TokenClient;
using Bank.Common.Extensions;
using Bank.Common.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Core.Http.Client
{
    public static class RegisterCoreClientExtension
    {
        public static WebApplicationBuilder AddCoreClient(this WebApplicationBuilder builder)
        {
            builder.BindOptions<CoreClientOptions>();
            builder.AddAuthTokenClient();

            builder
                .Services.AddHttpClient<CoreClient>()
                .AddResilience()
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, sslErrs) => true
                })
                .AddHttpMessageHandler<TracingHandler>()
                .AddHttpMessageHandler<AuthorizationHandler>();

            return builder;
        }
    }
}
