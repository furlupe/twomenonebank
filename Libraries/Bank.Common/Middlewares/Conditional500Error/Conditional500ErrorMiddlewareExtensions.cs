using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Bank.Common.Middlewares.Conditional500Error
{
    public static class UseProbable500ErrorMiddlewareExtension
    {
        /// <summary>
        /// Registers <see cref="Conditional500ErrorMiddleware"/> 
        /// </summary>
        /// <remarks>
        /// Uses probabilty function as default requestPermitter for the middleware:<br/>
        /// In 50% of the cases request will fail with 500. <br/>
        /// On even minutes the probability goes up to 90%
        /// </remarks>
        public static WebApplication UseConditional500ErrorMiddleware(this WebApplication app)
        {
            static bool permitter(HttpContext httpContext)
            {
                Random rng = new();
                bool isNowMinuteEven = DateTime.Now.TimeOfDay.Minutes % 2 == 0;

                var randomValue = rng.NextDouble();
                bool isValueWithinRange = isNowMinuteEven ? randomValue <= 0.9 : randomValue <= 0.5;

                return !isValueWithinRange;
            }

            return app.UseConditional500ErrorMiddleware(permitter);
        }

        /// <summary>
        /// Registers <see cref="Conditional500ErrorMiddleware"/> 
        /// </summary>
        /// <param name="requestPermitter">A delegate that determines whether or not the request should proceed further</param>
        public static WebApplication UseConditional500ErrorMiddleware(this WebApplication app, Func<HttpContext, bool> requestPermitter)
        {
            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/swagger"),
                builder => builder.UseMiddleware<Conditional500ErrorMiddleware>(requestPermitter)
            );

            return app;
        }
    }
}
