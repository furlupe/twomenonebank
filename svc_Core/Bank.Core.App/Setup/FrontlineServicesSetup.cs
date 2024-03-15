using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies.Handlers;
using Bank.Common.Extensions;
using Bank.Core.App.Services;
using System.Reflection;

namespace Bank.Core.App.Setup;

public static class FrontlineServicesSetup
{
    public static WebApplicationBuilder AddFrontlineServices(this WebApplicationBuilder builder)
    {
        builder.ConfigureAuth();

        var services = builder.Services;
        services.AddHealthChecks();
        services.AddScoped<IUserService, UserService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(o => o.AddAuth().UseXmlComments(Assembly.GetExecutingAssembly()));

        return builder;
    }

    public static void UseFrontlineServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapHealthChecks("/health");

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}
