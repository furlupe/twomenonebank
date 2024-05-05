using Bank.Attributes.Attributes;

namespace Bank.Notifications.Http.Client
{
    [ConfigurationModel("Notifications")]
    public class NotificationsClientOptions
    {
        public string Host { get; set; }
    }
}
