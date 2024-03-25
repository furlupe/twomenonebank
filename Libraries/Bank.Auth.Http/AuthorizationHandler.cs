using System.Net.Http.Headers;
using Bank.Auth.Http.TokenClient;

namespace Bank.Auth.Http
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly AuthTokenClient _tokenClient;
        public AuthorizationHandler(AuthTokenClient tokenClient)
        {
            _tokenClient = tokenClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            string token = await _tokenClient.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
