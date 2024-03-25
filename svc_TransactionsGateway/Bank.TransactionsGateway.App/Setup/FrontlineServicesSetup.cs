using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Common.Constants;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.TransactionsGateway.App.Services;

namespace Bank.TransactionsGateway.App.Setup;

public static class FrontlineServicesSetup
{
    public static WebApplicationBuilder AddFrontlineServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureAuth(x => { });

        var services = builder.Services;
        services.AddHealthChecks();
        services.AddControllers().AddJsonOptions(JsonConfigurationDefaults.JsonOptions);
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

        return builder;
    }

    public static void UseFrontlineServices(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapHealthChecks("/health");

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
