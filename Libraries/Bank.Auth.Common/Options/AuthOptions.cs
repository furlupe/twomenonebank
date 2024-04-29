using Bank.Attributes.Attributes;

namespace Bank.Auth.Common.Options
{
    [ConfigurationModel("Auth")]
    public class AuthOptions
    {
        public string Host { get; set; } = null!;
        public string Key { get; set; } = null!;
        public string Secret { get; set; } = null!;
    }
}
