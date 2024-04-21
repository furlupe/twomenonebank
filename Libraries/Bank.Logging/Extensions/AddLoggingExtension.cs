using Bank.Common.Extensions;
using Bank.Logging.Middlewares;
using Bank.Monitoring.Http;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Bank.Logging.Extensions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            var loggingOptions = builder.GetConfigurationValue<MonitoringServiceHttpOptions>();

            builder.Services.AddSerilog(
                (sp, opt) => opt
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] TRACE: {BankTraceId} {Message:lj}{NewLine}{Exception}")
                    .WriteTo.MonitoringService(loggingOptions.Url)
                    .Enrich.FromLogContext()
            );

            return builder;
        }

        public static WebApplication UseLogging(this WebApplication app)
        {
            app.UseMiddleware<TraceIdContextEnrichMiddleware>();

            return app;
        }
    }
}
