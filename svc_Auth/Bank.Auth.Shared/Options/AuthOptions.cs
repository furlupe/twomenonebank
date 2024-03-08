using Bank.Attributes.Attributes;

namespace Bank.Auth.Shared.Options
{
    [ConfigurationModel("Auth")]
    public class AuthOptions
    {
        public string Host { get; set; }
        public string Key { get; set; }
    }
}
