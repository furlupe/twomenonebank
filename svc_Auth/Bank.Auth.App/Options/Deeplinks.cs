using Bank.Attributes.Attributes;

namespace Bank.Auth.App.Options
{
    [ConfigurationModel("Deeplinks")]
    public class Deeplinks
    {
        public List<string> Links { get; set; }
    }
}
