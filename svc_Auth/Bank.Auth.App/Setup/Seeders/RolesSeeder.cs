using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace Bank.Auth.App.Setup.Seeders
{
    public class RolesSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RolesSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BankAuthDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

            foreach (string role in Enum.GetNames(typeof(Role)))
            {
                if (await manager.RoleExistsAsync(role))
                {
                    continue;
                }

                await manager.CreateAsync(new UserRole() { Name = role });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
