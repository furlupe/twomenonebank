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
}
