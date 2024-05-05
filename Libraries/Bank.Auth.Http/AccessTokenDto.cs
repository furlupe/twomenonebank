using System.Text.Json.Serialization;

namespace Bank.Auth.Http
{
    public class AccessTokenDto
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; } = null!;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = null!;

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
