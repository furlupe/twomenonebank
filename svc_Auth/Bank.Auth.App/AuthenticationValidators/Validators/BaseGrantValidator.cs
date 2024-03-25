using OpenIddict.Abstractions;

namespace Bank.Auth.App.AuthenticationValidators.Validators
{
    public abstract class BaseGrantValidator
    {
        public async Task<GrantValidationResult> ValidateAsync(OpenIddictRequest request)
        {
            try
            {
                return await CommitValidation(request);
            }
            catch(Exception ex)
            {
                return GrantValidationResult.Failure(ex.Message);
            }
        }

        protected abstract Task<GrantValidationResult> CommitValidation(OpenIddictRequest request);
    }
}
