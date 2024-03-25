using Bank.Auth.Common.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Bank.Auth.Http.TokenClient
{
    public class AuthTokenClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _rootUrl;
        private readonly string _secret;
        public AuthTokenClient(
            HttpClient httpClient,
            IOptions<AuthOptions> options
        )
        {
            _httpClient = httpClient;
            _rootUrl = options.Value.Host;
            _secret = options.Value.Secret;
        }

        public async Task<string> GetToken()
        {
            string edp = $"{_rootUrl}/connect/token";

            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", "innerservice"),
                new KeyValuePair<string, string>("client_secret", _secret)
            });

            var response = await _httpClient.PostAsync(edp, data);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(content);
            }

            return JsonSerializer.Deserialize<AccessTokenDto>(content).Token;
        }
    }
}
