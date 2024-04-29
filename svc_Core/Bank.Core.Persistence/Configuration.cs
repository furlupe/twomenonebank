using Bank.Attributes.Attributes;

namespace Bank.Core.Persistence;

[ConfigurationModel("CoreDb")]
public class Configuration
{
    public string ConnectionString { get; set; } = null!;
}
