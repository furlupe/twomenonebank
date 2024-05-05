using Bank.Attributes.Attributes;

namespace Bank.Core.Http.Client
{
    [ConfigurationModel("Core")]
    public class CoreClientOptions
    {
        public string Host { get; set; } = null!;
    }
}
