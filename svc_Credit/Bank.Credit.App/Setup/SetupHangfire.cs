using Bank.Common.Extensions;
using Bank.Credit.App.Services;
using Hangfire;
using Hangfire.PostgreSql;

namespace Bank.Credit.App.Setup
{
    public static class SetupHangfire
    {
        public static void ConfigureHangfire(this WebApplicationBuilder builder)
        {
            builder
                .Services.AddHangfire(config =>
                    config.UsePostgreSqlStorage(c =>
                        c.UseNpgsqlConnection(
                            builder.GetConfigurationValue<DbConnection>().ConnectionString
                        )
                    )
                )
                .AddHangfireServer();
        }

        public static void SetupCreditJobs(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<CreditBackroundService>();
            RecurringJob.AddOrUpdate(
                "Credit processing",
                () => service.ProcessOpenCredits(),
                Cron.Daily
            );
        }
    }
}
