using Bank.Auth.Common.Options;
using Bank.Common.DateTimeProvider;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Bank.Auth.Http.TokenClient
{
    public class AuthTokenClient
    {
        private readonly HttpClient _httpClient;
        private readonly TokenCache _cache;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly string _rootUrl;
        private readonly string _secret;

        private readonly string _cacheKey = "token";

        public AuthTokenClient(
            HttpClient httpClient,
            IOptions<AuthOptions> options,
            TokenCache cache,
            IDateTimeProvider dateTimeProvider
        )
        {
            _httpClient = httpClient;
            _rootUrl = options.Value.Host;
            _secret = options.Value.Secret;
            _cache = cache;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<string> GetToken()
        {
            var cacheValueExists = _cache.TryGet(_cacheKey, _dateTimeProvider.UtcNow, out var tokenValue);
            return cacheValueExists && tokenValue != null ? tokenValue : await RequestToken();
        }

        private async Task<string> RequestToken()
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

            var tokenDto = JsonSerializer.Deserialize<AccessTokenDto>(content) ?? throw new HttpRequestException(content);

            _cache.AddOrUpdate(_cacheKey, tokenDto.Token, _dateTimeProvider.UtcNow + TimeSpan.FromSeconds(tokenDto.ExpiresIn));

            return tokenDto.Token;
        }
    }
}
