using System.Reflection;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Common;
using Bank.Common.Constants;
using Bank.Common.Extensions;
using Bank.Common.Middlewares;
using Bank.Common.Middlewares.Conditional500Error;
using Bank.Core.App.Hubs;
using Bank.Core.App.Services;

namespace Bank.Core.App.Setup;

public static class FrontlineServicesSetup
{
    public static WebApplicationBuilder AddFrontlineServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureAuth().AddUserCreationPolicy();

        var services = builder.Services;
        services.AddHealthChecks();
        services.AddScoped<IUserService, UserService>();
        services.AddControllers().AddJsonOptions(JsonConfigurationDefaults.JsonOptions);
        services.AddSignalR();
        services.AddCors(x =>
        {
            x.AddDefaultPolicy(policy =>
            {
                policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            });
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

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

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<TransactionsHub>("accounts");
    }
}
