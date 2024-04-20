using Bank.Monitoring.Domain;
using Elastic.Clients.Elasticsearch;

namespace Bank.Monitoring.App.Extensions
{
    public static class ElasticClientExtension
    {
        public static WebApplicationBuilder AddElasticClient(
            this WebApplicationBuilder builder
        )
        {
            var settings = new ElasticsearchClientSettings(new Uri("http://localhost:9200"))
                .DefaultMappingFor<Log>(l => l.IndexName("logs"));
            var client = new ElasticsearchClient(settings);

            builder.Services.AddSingleton(client);

            return builder;
        }
    }
}
