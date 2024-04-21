using Bank.Common.Extensions;
using Bank.Monitoring.App.Dto;
using Bank.Monitoring.App.Options;
using Elastic.Clients.Elasticsearch;

namespace Bank.Monitoring.App.Extensions
{
    public static class ElasticClientExtension
    {
        public static WebApplicationBuilder AddElasticClient(this WebApplicationBuilder builder)
        {
            var esSettings = builder.GetConfigurationValue<ElasticsearchOptions>();

            var settings = new ElasticsearchClientSettings(
                new Uri(esSettings.Host)
            ).DefaultMappingFor<Log>(l => l.IndexName("logs"));

            var client = new ElasticsearchClient(settings);

            builder.Services.AddSingleton(client);

            return builder;
        }
    }
}
