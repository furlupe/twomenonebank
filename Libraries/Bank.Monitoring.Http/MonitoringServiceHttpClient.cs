using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Sinks.Http;
using System.Text.Json;

namespace Bank.Monitoring.Http
{
    public class MonitoringServiceHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        public MonitoringServiceHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public void Configure(IConfiguration configuration) { }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, Stream contentStream, CancellationToken cancellationToken)
        {
            using var content = new StreamContent(contentStream);
            content.Headers.Add("Content-Type", "application/json");

            var response = await _httpClient
              .PostAsync(requestUri, content, cancellationToken);

            return response;
        }
    }
}
