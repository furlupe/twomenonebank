using Bank.Auth.Http;
using Bank.Auth.Http.TokenClient;
using Bank.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.TransactionsGateway.Http.Client
{
    public static class RegisterTransactionsClientExtension
    {
        public static WebApplicationBuilder AddTransactionsClient(this WebApplicationBuilder builder)
        {
            builder.BindOptions<TransactionsClientOptions>();
            builder.AddAuthTokenClient();

            builder.Services.AddHttpClient<TransactionsClient>()
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, sslErrs) => true
                })
                .AddHttpMessageHandler<AuthorizationHandler>();

            return builder;
        }
    }
}
