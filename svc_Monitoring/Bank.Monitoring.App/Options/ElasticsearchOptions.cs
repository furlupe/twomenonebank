using Bank.Attributes.Attributes;

namespace Bank.Monitoring.App.Options
{
    [ConfigurationModel("Elasticsearch")]
    public class ElasticsearchOptions
    {
        public string Host { get; set; }
    }
}
