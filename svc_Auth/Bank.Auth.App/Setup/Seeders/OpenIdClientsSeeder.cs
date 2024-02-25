using Bank.Auth.Domain;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Setup.Seeders
{
    public class OpenIdClientsSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public OpenIdClientsSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BankAuthDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("amogus", cancellationToken) is null)
            {
                await manager.CreateAsync(
                    new OpenIddictApplicationDescriptor
                    {
                        ClientId = "amogus",
                        Permissions =
                        {
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.Password,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.Scopes.Profile
                        }
                    },
                    cancellationToken
                );
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
