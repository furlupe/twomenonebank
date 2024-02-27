using Bank.Auth.App.Services.Auth.Validators;
using Bank.Auth.App.Services.Auth.Validators.Result;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupServices
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder
                .Services.AddTransient<GrantValidatorFactory>()
                .AddTransient<GrantValidationResultFactory>();

            return builder;
        }
    }
}
