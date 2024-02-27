using Bank.Auth.App.Setup.Extensions;
using MassTransit;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupMassTransit
    {
        public static WebApplicationBuilder AddMassTransit(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddMassTransit(x => x.UsingRabbitMq());

            builder.Services
                .AddOptions<RabbitMqTransportOptions>()
                .BindConfiguration("RabbitMq");

            return builder;
        }
    }
}
