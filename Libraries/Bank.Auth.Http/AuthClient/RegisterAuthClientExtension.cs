﻿using Bank.Auth.Common.Options;
using Bank.Auth.Http.TokenClient;
using Bank.Common.Extensions;
using Bank.Common.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Auth.Http.AuthClient
{
    public static class RegisterAuthClientExtension
    {
        public static WebApplicationBuilder AddAuthClient(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.BindOptions<AuthOptions>();
            builder.AddAuthTokenClient();

            builder
                .Services.AddHttpClient<AuthClient>()
                .AddResilience()
                .ConfigurePrimaryHttpMessageHandler(c => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (msg, cert, chain, sslErrs) => true
                })
                .AddHttpMessageHandler<TracingHandler>()
                .AddHttpMessageHandler<AuthorizationHandler>();

            return builder;
        }
    }
}
