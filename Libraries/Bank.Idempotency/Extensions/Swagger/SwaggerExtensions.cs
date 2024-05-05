using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.Idempotency.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static SwaggerGenOptions AddIdempotencyHeader(this SwaggerGenOptions options)
        {
            options.OperationFilter<IdempotencyHeaderFilter>();

            return options;
        }
    }
}
