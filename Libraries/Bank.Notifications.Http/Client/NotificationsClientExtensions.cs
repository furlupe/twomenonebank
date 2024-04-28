using Microsoft.AspNetCore.Builder;
using Bank.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Bank.Common.Http;
using Bank.Auth.Http;
using Bank.Auth.Http.TokenClient;
using Bank.Auth.Common.Options;

namespace Bank.Notifications.Http.Client
{
    public static class NotificationsClientExtensions
    {
        public static WebApplicationBuilder AddNotificationsClient(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.BindOptions<AuthOptions>();
            builder.BindOptions<NotificationsClientOptions>();
            builder.AddAuthTokenClient();

            builder
                .Services.AddHttpClient<NotificationsClient>()
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
