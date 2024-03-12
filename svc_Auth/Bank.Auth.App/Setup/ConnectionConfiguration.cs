using Bank.Attributes.Attributes;

namespace Bank.Auth.App.Setup
{
    [ConfigurationModel("AuthDb")]
    public class ConnectionConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
