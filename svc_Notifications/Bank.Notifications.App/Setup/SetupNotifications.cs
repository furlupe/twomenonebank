using Bank.Notifications.App.Services;

namespace Bank.Notifications.App.Setup
{
    public static class SetupNotifications
    {
        public static WebApplicationBuilder AddNotifications(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<BankNotificationService>();

            return builder;
        }
    }
}
