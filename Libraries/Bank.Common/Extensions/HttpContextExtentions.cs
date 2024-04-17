using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Bank.Common.Extensions
{
    public static class HttpContextExtentions
    {
        public static string GetTraceId(this HttpContext httpContext)
        {
            var traceId = httpContext.Request.Headers.TraceParent.ToString();
            if (string.IsNullOrEmpty(traceId))
            {
                traceId = Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier;
            }

            return traceId ?? Guid.NewGuid().ToString();
        }
    }
}
