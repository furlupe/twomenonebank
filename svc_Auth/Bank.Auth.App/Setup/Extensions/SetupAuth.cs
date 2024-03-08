﻿using Bank.Auth.App.Setup.Seeders;
using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Options;
using Bank.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Validation.AspNetCore;

namespace Bank.Auth.App.Setup.Extensions
{
    public static class SetupAuth
    {
        public static WebApplicationBuilder ConfigureAuth(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

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
                    options.AllowPasswordFlow().AllowRefreshTokenFlow();

                    options
                        .DisableAccessTokenEncryption()
                        .AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.AddSigningKey(
                        new SymmetricSecurityKey(Convert.FromBase64String(authOptions.Key))
                    );

                    options
                        .UseAspNetCore()
                        .DisableTransportSecurityRequirement()
                        .EnableTokenEndpointPassthrough();
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
            services.AddAuthorization(options => options.RegisterPolicies());

            services
                .AddHostedService<OpenIdClientsSeeder>()
                .AddHostedService<UserSeeder>()
                .AddHostedService<RolesSeeder>();

            return builder;
        }
    }
}
