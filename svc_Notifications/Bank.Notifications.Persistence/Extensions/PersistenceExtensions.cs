using Bank.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Notifications.Persistence.Extensions
{
    public static class PersistenceExtensions
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BankNotificationsDbContext>(
                options => options.UseNpgsql(builder.GetConfigurationValue<DbConnection>().ConnectionString)
            );

            return builder;
        }
        public static async Task UsePersistence(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<BankNotificationsDbContext>();
            await db.Database.MigrateAsync();
        }
    }

}
