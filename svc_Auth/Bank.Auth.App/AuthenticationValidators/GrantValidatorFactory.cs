using Bank.Auth.App.AuthenticationValidators.Validators;
using OpenIddict.Abstractions;

namespace Bank.Auth.App.AuthenticationValidators
{
    public class GrantValidatorFactory
    {
        private IHttpContextAccessor _httpContextAccessor;

        public GrantValidatorFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseGrantValidator Create(OpenIddictRequest request)
        {
            if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                return new AuthorizationCodeGrantValidator(_httpContextAccessor);
            }

            if (request.IsClientCredentialsGrantType())
            {
                return new ClientCredentialsGrantValidator();
            }

            throw new InvalidOperationException("unsupported_grant");
        }
    }
}
