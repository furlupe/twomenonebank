using Bank.Common.Extensions;
using Bank.Core.Http.Dto;
using Microsoft.Extensions.Options;

namespace Bank.Core.Http.Client
{
    public class CoreClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CoreClient(HttpClient httpClient, IOptions<CoreClientOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.Host;
        }

        public async Task<AccountDto> GetMasterAccountInfo()
        {
            var result = await _httpClient.GetAsJson<AccountDto>($"{_baseUrl}/manage/accounts/master") 
                    ?? throw new InvalidOperationException("Null master account");
            return result;
        }
    }
}
