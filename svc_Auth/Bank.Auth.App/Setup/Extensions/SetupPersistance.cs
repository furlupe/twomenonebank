using Bank.Auth.Domain;
using Bank.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupPersistance
    {
        public static WebApplicationBuilder AddPersistance(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BankAuthDbContext>(options =>
            {
                options.UseNpgsql(
                    builder.GetConfigurationValue<ConnectionConfiguration>().ConnectionString
                );
                options.UseOpenIddict();
            });

            return builder;
        }

        public static async Task UsePersistance(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<BankAuthDbContext>())
                {
                    await db.Database.MigrateAsync();
                }
            }
        }
    }
}
