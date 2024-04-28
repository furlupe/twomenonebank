using Bank.Attributes.Attributes;

namespace Bank.Notifications.Persistence.Extensions
{
    [ConfigurationModel("NotificationsDb")]
    public class DbConnection
    {
        public string ConnectionString { get; set; }
    }
}
