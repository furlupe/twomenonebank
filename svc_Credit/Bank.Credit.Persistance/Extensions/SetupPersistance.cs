using Bank.Common.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Credit.Persistance.Extensions
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
