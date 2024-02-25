using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Bank.Auth.App.Setup.Seeders
{
    public class UserSeeder : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public UserSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BankAuthDbContext>();
            await context.Database.EnsureCreatedAsync(cancellationToken);

            var manager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (await manager.FindByNameAsync("amogus@mail.ru") == null)
            {
                User user = new() { Email = "amogus@mail.ru", UserName = "amogus@mail.ru" };

                await manager.CreateAsync(user);
                await manager.AddPasswordAsync(user, "balls");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
