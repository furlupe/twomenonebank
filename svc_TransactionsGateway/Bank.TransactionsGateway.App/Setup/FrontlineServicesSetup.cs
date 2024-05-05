using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Common;
using Bank.Common.Constants;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Idempotency.Extensions.Swagger;

namespace Bank.TransactionsGateway.App.Setup;

public static class FrontlineServicesSetup
{
    public static WebApplicationBuilder AddFrontlineServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureAuth();

        var services = builder.Services;

        services.AddHealthChecks();
        services.AddControllers().AddJsonOptions(JsonConfigurationDefaults.JsonOptions);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.AddAuth().AddIdempotencyHeader().UseXmlComments(Assembly.GetExecutingAssembly()));

        return builder;
    }

    public static void UseFrontlineServices(this WebApplication app)
    {
        if (app.TransientErrorsEnabled())
            app.UseConditional500ErrorMiddleware();
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapHealthChecks("/health");

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
