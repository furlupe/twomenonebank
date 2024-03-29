using Bank.Auth.App.AuthenticationValidators;
using Bank.Auth.App.Options;
using Bank.Auth.App.Setup.Seeders;
using Bank.Auth.Common.Options;
using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Bank.Common.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupAuth
    {
        public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
        {
            builder.BindOptions<Deeplinks>();

            var services = builder.Services;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.LoginPath = "/login";
                    }
                );

            services.AddAuthorization();

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
                    options
                        .AllowRefreshTokenFlow()
                        .AllowAuthorizationCodeFlow()
                        .AllowClientCredentialsFlow();

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

            services.AddHostedService<OpenIdClientsSeeder>().AddHostedService<UserSeeder>();
            services.AddHttpContextAccessor().AddScoped<GrantValidatorFactory>();

            return builder;
        }

        public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
        {
            return app.UseAuthentication();
        }
    }
}
