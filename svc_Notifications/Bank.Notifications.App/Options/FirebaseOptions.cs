using Bank.Attributes.Attributes;

namespace Bank.Notifications.App.Options
{
    [ConfigurationModel("Firebase")]
    public class FirebaseOptions
    {
        public string ProjectId { get; set; }
    }
}
