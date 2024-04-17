using Bank.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Bank.Common.Http
{
    public class BaseHttpClient
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseHttpClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            _httpClient.DefaultRequestHeaders.Add(HeaderNames.TraceParent, httpContextAccessor.HttpContext?.GetTraceId());
        }
    }
}
