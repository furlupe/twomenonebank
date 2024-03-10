using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Core.Persistence;

public static class PersistenceSetup
{
    public static IServiceCollection AddCoreDbContext(
        this IServiceCollection services,
        Configuration config
    )
    {
        services.AddDbContext<CoreDbContext>(
            (options) =>
            {
                options.UseNpgsql(config.ConnectionString);
            },
            contextLifetime: ServiceLifetime.Scoped,
            optionsLifetime: ServiceLifetime.Singleton
        );

        return services;
    }

    public static async Task UseCoreDbContext(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using (var db = scope.ServiceProvider.GetRequiredService<CoreDbContext>())
            {
                await db.Database.MigrateAsync();
            }
        }
    }
}
