using Bank.Monitoring.App.Dto;
using Bank.Monitoring.Domain;
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
        public async Task CreateLog([FromBody] LogDto log)
        {
            await _elasticsearchClient.IndexAsync(
                new Log()
                {
                    CreatedAt = log.CreatedAt,
                    TraceId = log.TraceId,
                    Message = log.Message
                }
            );
        }
    }
}
