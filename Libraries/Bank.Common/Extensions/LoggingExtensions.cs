using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Bank.Common.Extensions
{
    public static class LoggingExtensions
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Services.AddSerilog(
                opt => opt
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] TRACE: {BankTraceId} {Message:lj}{NewLine}{Exception}")
                    .Enrich.FromLogContext()
            );

            return builder;
        }
    }
}
