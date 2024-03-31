using Bank.Common.Extensions;
using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace Bank.Amqp;

public static class MassTransitExtensions
{
    public static WebApplicationBuilder AddMassTransit(
        this WebApplicationBuilder builder,
        Action<IBusRegistrationConfigurator>? configure = null,
        Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? configureRabbitMq = null
    )
    {
        builder.Services.AddMassTransit(x =>
        {
            x.UsingRabbitMq(
                (ctx, cfg) =>
                {
                    cfg.ConfigureEndpoints(ctx);
                    configureRabbitMq?.Invoke(ctx, cfg);
                }
            );
            configure?.Invoke(x);
        });

        builder.BindOptions<RabbitMqTransportOptions>("RabbitMq");

        return builder;
    }
}
