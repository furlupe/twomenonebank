using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Bank.Common.Extensions;

namespace Bank.Logging.Middlewares
{
    public class TraceIdContextEnrichMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceIdContextEnrichMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("BankTraceId", context.GetTraceId()))
            {
                await _next(context);
            }
        }
    }
}
