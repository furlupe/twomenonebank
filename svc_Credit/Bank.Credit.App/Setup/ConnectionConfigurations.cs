using Bank.Attributes.Attributes;

namespace Bank.Credit.App.Setup
{
    [ConfigurationModel("CreditDb")]
    public class DbConnection
    {
        public string ConnectionString { get; set; }
    }
}
