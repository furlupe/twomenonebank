using Bank.Auth.Common.Options;
using Bank.Auth.Common.Policies.Handlers;
using Bank.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.Common.Extensions
{
    public static class ConfigureAuthExtensions
    {
        /// <summary>
        /// Overload for default ConfigureAuth that requires "Auth" section in appsettings.json or any configuration provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static WebApplicationBuilder ConfigureAuth(
            this WebApplicationBuilder builder
        )
        {
            var authOptions = builder.GetConfigurationValue<AuthOptions>();

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(authOptions.Key));

            return builder.ConfigureAuth(authOptions.Host, securityKey);
        }

        public static WebApplicationBuilder AddUserCreationPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorizationHandler, CreateUserAuthorizationHandler>();
            builder.Services.AddAuthorizationBuilder()
                .AddPolicy(Policies.Policies.CreateUserIfNeeded,
                builder => builder.AddRequirements(new CreateUserAuthorizationRequirement())
            );

            return builder;
        }

        /// <summary>
        /// Default method for configuration auth, adds jwt handling and stuff
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="issuer"></param>
        /// <param name="issuerSigningKey"></param>
        /// <returns></returns>
        public static WebApplicationBuilder ConfigureAuth(
            this WebApplicationBuilder builder,
            string issuer,
            SecurityKey issuerSigningKey
        )
        {
            var services = builder.Services;

            services
                .AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.Authority = issuer;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidIssuers = new List<string>() { issuer },
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = issuerSigningKey
                        };

                        // leave for [Authorize] debugging: set breakpoint on lambda
#pragma warning disable
                        options.Events = new()
                        {
                            OnAuthenticationFailed = async ctx =>
                            {
                                var breakpoint = true;
                            }
                        };
#pragma warning restore
                    }
                );

            services.AddAuthorization();
            return builder;
        }
    }
}
