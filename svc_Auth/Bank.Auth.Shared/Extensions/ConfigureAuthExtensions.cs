using Bank.Auth.Shared.Policies.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.Shared.Extensions
{
    public static class ConfigureAuthExtensions
    {
        /// <summary>
        /// Overload for default ConfigureAuth that requires "Auth" section in appsettings.json or any configuration provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
        {
            string? issuer = builder.Configuration.GetValue<string>("Auth:Host");
            string? key = builder.Configuration.GetValue<string>("Auth:Key");

            if (issuer == null)
            {
                throw new ArgumentNullException($"No issuer specified for {nameof(ConfigureAuth)}");
            }

            if (key == null)
            {
                throw new ArgumentNullException(
                    $"No singing key specified for {nameof(ConfigureAuth)}"
                );
            }

            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));

            return builder.ConfigureAuth(issuer, securityKey);
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

            services.AddScoped<IAuthorizationHandler, CreateUserAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Policies.EmployeeOrHigher, builder =>
                 {
                     builder.AddRequirements(
                         new RoleAuthorizationRequirement([Enumerations.Role.Admin, Enumerations.Role.Employee])
                     );
                 });

                 options.AddPolicy(Policies.Policies.CreateUserIfNeeded, builder =>
                    {
                        builder.AddRequirements(
                            new CreateUserAuthorizationRequirement()
                        );
                    });
            });


            return builder;
        }
    }
}
