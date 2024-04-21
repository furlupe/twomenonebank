using Bank.Monitoring.App.Dto;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Monitoring.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ElasticsearchClient _elasticsearchClient;

        public LogController(ElasticsearchClient elasticsearchClient)
        {
            _elasticsearchClient = elasticsearchClient;
        }

        [HttpPost]
        public async Task CreateLog([FromBody] LogDto[] logs)
        {
            await _elasticsearchClient.IndexManyAsync(
                logs.Select(l => new Log()
                {
                    Timestamp = l.Timestamp,
                    Level = l.Level,
                    RenderedMessage = l.RenderedMessage,
                    Properties = l.Properties
                })
            );
        }
    }
}
