using Bank.Common.Extensions;
using Bank.Credit.App.BackgroundTasks;
using Bank.Credit.App.Services;
using Bank.Credit.Persistance.Extensions;
using Hangfire;
using Hangfire.PostgreSql;

namespace Bank.Credit.App.Setup
{
    public static class SetupHangfire
    {
        public static void ConfigureHangfire(this WebApplicationBuilder builder)
        {
            builder
                .Services.AddTransient<CreditBackroundService>()
                .AddTransient<CreditRatingBackgroundService>();

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
            var creditService = scope.ServiceProvider.GetRequiredService<CreditBackroundService>();
            RecurringJob.AddOrUpdate(
                "Credit processing",
                () => creditService.ProcessOpenCredits(),
                Cron.Daily
            );

            var creditRatingService =
                scope.ServiceProvider.GetRequiredService<CreditRatingBackgroundService>();
            RecurringJob.AddOrUpdate(
                "Credit rating processing",
                () => creditRatingService.RecalculateRatings(),
                Cron.Daily
            );
        }
    }
}
