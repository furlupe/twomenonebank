using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Bank.Common.Middlewares.Tracing
{
    public class TracingMiddleware
    {
        private readonly RequestDelegate _next;

        public TracingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var traceparent = context.Request.Headers.TraceParent;
            if (string.IsNullOrEmpty(traceparent))
            {
                context.Request.Headers.TraceParent = Activity.Current?.TraceId.ToString() ?? context.TraceIdentifier;
            }

            await _next(context);
        }
    }
}
