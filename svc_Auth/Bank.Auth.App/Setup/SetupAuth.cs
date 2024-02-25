using Bank.Auth.App.Setup.Seeders;
using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.App.Setup
{
    public static class SetupAuth
    {
        public static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

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
                    options.AllowPasswordFlow().AllowRefreshTokenFlow();

                    options
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.UseAspNetCore().EnableTokenEndpointPassthrough();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });
            services.AddAuthorization();

            services
                .AddHostedService<OpenIdClientsSeeder>()
                .AddHostedService<UserSeeder>()
                .AddHostedService<RolesSeeder>();

            return builder;
        }
    }
}
