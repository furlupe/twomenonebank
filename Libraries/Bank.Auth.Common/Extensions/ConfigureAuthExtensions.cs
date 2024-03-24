using Bank.Auth.Common.Enumerations;
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
            this WebApplicationBuilder builder, 
            Action<AuthorizationOptions>? authSettings = null
        )
        {
            var authOptions = builder.GetConfigurationValue<AuthOptions>();

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(authOptions.Key));

            return builder.ConfigureAuth(authOptions.Host, securityKey, authSettings);
        }

        public static AuthorizationOptions RegisterPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(
                Policies.Policies.EmployeeOrHigher,
                builder =>
                {
                    builder.AddRequirements(
                        new RoleAuthorizationRequirement([Role.Admin, Role.Employee])
                    );
                }
            );

            options.AddPolicy(
                Policies.Policies.CreateUserIfNeeded,
                builder =>
                {
                    builder.AddRequirements(new CreateUserAuthorizationRequirement());
                }
            );

            return options;
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
            SecurityKey issuerSigningKey,
            Action<AuthorizationOptions>? authSettings = null
        )
        {
            authSettings ??= opt => { };
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

            services.AddScoped<IAuthorizationHandler, CreateUserAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();

            services.AddAuthorization(authSettings);

            return builder;
        }
    }
}
