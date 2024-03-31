using Bank.Attributes.Attributes;

namespace Bank.TransactionsGateway.Http.Client
{
    [ConfigurationModel("Transactions")]
    public class TransactionsClientOptions
    {
        public string Host { get; set; }
    }
}
