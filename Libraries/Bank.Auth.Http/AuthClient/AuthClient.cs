using Bank.Auth.Common.Options;
using Bank.Common.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bank.Auth.Http.AuthClient
{
    public class AuthClient : BaseHttpClient
    {
        private readonly string _rootUrl;

        public AuthClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor , IOptions<AuthOptions> options)
            : base(httpClient, httpContextAccessor)
        {
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
