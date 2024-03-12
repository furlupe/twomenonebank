using Bank.Common.Extensions;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Setup
{
    public static class SetupPersistance
    {
        public static WebApplicationBuilder AddPersistance(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BankCreditDbContext>(options =>
                options.UseNpgsql(builder.GetConfigurationValue<DbConnection>().ConnectionString)
            );

            return builder;
        }

        public static async Task UsePersistance(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var db = scope.ServiceProvider.GetRequiredService<BankCreditDbContext>())
                {
                    await db.Database.MigrateAsync();
                }
            }
        }
    }
}
