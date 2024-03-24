using Bank.Attributes.Attributes;

namespace Bank.Auth.Common.Options
{
    [ConfigurationModel("Auth")]
    public class AuthOptions
    {
        public string Host { get; set; }
        public string Key { get; set; }
    }
}
