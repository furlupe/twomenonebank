using Microsoft.AspNetCore.Http;

namespace Bank.Common.Middlewares.Conditional500Error
{
    /// <summary>
    /// A middleware that returns 500 by condition calculated via provided delegate
    /// </summary>
    public class Conditional500ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Func<HttpContext, bool> _requestPermitter;

        /// <summary>
        /// Initializes new Conditional500ErrorMiddleware
        /// </summary>
        /// <param name="requestPermitter">A delegate that determines whether or not the request should proceed further</param>
        public Conditional500ErrorMiddleware(RequestDelegate next, Func<HttpContext, bool> requestPermitter)
        {
            _next = next;
            _requestPermitter = requestPermitter;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_requestPermitter(context))
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }

            await _next(context);
        }
    }
}
