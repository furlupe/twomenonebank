using Bank.Auth.App.Options;
using Bank.Auth.Common.Options;
using Bank.Auth.Domain;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Setup.Seeders
{
    public class OpenIdClientsSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _innerClientSecret;

        public OpenIdClientsSeeder(
            IServiceProvider serviceProvider,
            IOptions<AuthOptions> tokenOptions
        )
        {
            _serviceProvider = serviceProvider;
            _innerClientSecret = tokenOptions.Value.Secret;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BankAuthDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            var mobileClient = new OpenIddictApplicationDescriptor()
            {
                ClientId = "amogus",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.Endpoints.Authorization,
                    Permissions.GrantTypes.Password,
                    Permissions.GrantTypes.RefreshToken,
                    Permissions.Scopes.Profile,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                }
            };

            var deeplinks = scope
                .ServiceProvider.GetRequiredService<IOptions<Deeplinks>>()
                .Value.Links;
            deeplinks.ForEach(link => mobileClient.RedirectUris.Add(new Uri(link)));

            List<OpenIddictApplicationDescriptor> clients =
            [
                mobileClient,
                new()
                {
                    ClientId = "innerservice",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials,
                    },
                    ClientSecret = _innerClientSecret
                }
            ];

            foreach (var client in clients)
            {
                var existingClient = await manager.FindByClientIdAsync(client.ClientId!, cancellationToken);
                if (existingClient != null)
                {
                    await manager.DeleteAsync(existingClient, cancellationToken);
                }

                await manager.CreateAsync(client, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
