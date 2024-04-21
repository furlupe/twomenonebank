using Serilog;
using Serilog.Configuration;

namespace Bank.Monitoring.Http
{
    public static class MonitoringServiceSerilogExtensions
    {
        public static LoggerConfiguration MonitoringService(this LoggerSinkConfiguration config, string requestUri)
        {
            return config.Http(
                requestUri: requestUri,
                queueLimitBytes: null,
                httpClient: new MonitoringServiceHttpClient()
            );
        }
    }
}
