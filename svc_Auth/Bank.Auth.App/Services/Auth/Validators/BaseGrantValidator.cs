﻿using Bank.Auth.App.Services.Auth.Validators.Result;
using OpenIddict.Abstractions;

namespace Bank.Auth.App.Services.Auth.Validators
{
    public abstract class BaseGrantValidator
    {
        protected readonly GrantValidationResultFactory _grantResultFactory;

        public BaseGrantValidator(GrantValidationResultFactory grantResultFactory)
        {
            _grantResultFactory = grantResultFactory;
        }

        public async Task<GrantValidationResult> ValidateAsync(OpenIddictRequest request)
        {
            try
            {
                var result = await CommitValidation(request);

                if (result.User != null && result.User.LockoutEnd != null)
                {
                    return _grantResultFactory.Banned();
                }

                return result;
            }
            catch (Exception ex)
            {
                return GrantValidationResult.Failure(ex.Message);
            }
        }

        protected abstract Task<GrantValidationResult> CommitValidation(OpenIddictRequest request);
    }
}
