using Bank.Attributes.Attributes;

namespace Bank.Monitoring.Http
{
    [ConfigurationModel("LoggingService")]
    public class MonitoringServiceHttpOptions
    {
        public string Url { get; set; }
    }
}
