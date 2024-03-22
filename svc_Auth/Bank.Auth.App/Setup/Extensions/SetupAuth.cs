using Bank.Auth.Domain.Models;
using Bank.Auth.Domain;
using Bank.Auth.Shared.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Bank.Common.Extensions;
using Microsoft.IdentityModel.Tokens;
using Bank.Auth.Shared.Extensions;
using Bank.Auth.App.Setup.Seeders;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupAuth
    {
        public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/login";
            });

            services.AddAuthorization(options => options.RegisterPolicies());

            var authOptions = builder.GetConfigurationValue<AuthOptions>();

            services
                .AddIdentity<User, UserRole>(options =>
                {
                    options.Lockout.AllowedForNewUsers = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<BankAuthDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });

            services
                .AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore().UseDbContext<BankAuthDbContext>();
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("/connect/token");
                    options.SetAuthorizationEndpointUris("/connect/authorize");
                    options.AllowRefreshTokenFlow().AllowAuthorizationCodeFlow().AllowPasswordFlow();

                    options
                        .DisableAccessTokenEncryption()
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.AddSigningKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(authOptions.Key))
                    );

                    options.SetIssuer(authOptions.Host);

                    options
                        .UseAspNetCore()
                        .DisableTransportSecurityRequirement()
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services
                .AddHostedService<OpenIdClientsSeeder>()
                .AddHostedService<UserSeeder>();

            return builder;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app) 
        {
            return app.UseAuthentication();
        }
    }
}
