using Bank.Attributes.Attributes;

namespace Bank.Credit.Persistance.Extensions
{
    [ConfigurationModel("CreditDb")]
    public class DbConnection
    {
        public string ConnectionString { get; set; }
    }
}
