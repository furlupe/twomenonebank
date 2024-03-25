using Bank.Auth.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bank.Auth.Http.AuthClient
{
    public class AuthClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _rootUrl;

        public AuthClient(HttpClient httpClient, IOptions<AuthOptions> options)
        {
            _httpClient = httpClient;
            _rootUrl = options.Value.Host;
        }

        public async Task<Guid> GetUserIdByPhone(string phoneNumber)
        {
            var result = await _httpClient.GetStringAsync($"{_rootUrl}/api/user/id" + QueryString.Create("phoneNumber", phoneNumber));
            result = result.Replace("\"", "");
            return Guid.Parse(result);
        }

        public async Task<string> Version()
        {
            return await _httpClient.GetStringAsync($"{_rootUrl}/api/version/authenticated-service");
        }
    }
}
