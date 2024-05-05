using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.Idempotency.Extensions.Swagger
{
    public class IdempotencyHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = IdempotencyConstants.Header,
                In = ParameterLocation.Header,
                Description = "Idempotence key",
                Required = true,
                Schema = new()
                {
                    Type = "string",
                    Format = "uuid",
                    Default = new OpenApiString(Guid.NewGuid().ToString())
                }
            });
        }
    }
}
