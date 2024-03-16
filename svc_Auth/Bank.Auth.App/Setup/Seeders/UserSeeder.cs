using System.Security.Claims;
using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
using Bank.Auth.Shared.Claims;
using Bank.Auth.Shared.Enumerations;
using Microsoft.AspNetCore.Identity;
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
                User user =
                    new()
                    {
                        Email = "amogus@mail.ru",
                        UserName = "amogus@mail.ru",
                        Role = Role.Admin.ToString()
                    };

                await manager.CreateAsync(user);
                await manager.AddPasswordAsync(user, "balls");

                List<Claim> claims =
                [
                    new Claim(Claims.Subject, user.Id.ToString()),
                    new Claim(Claims.Name, user.UserName),
                    new Claim(ClaimTypes.Role, Role.Admin.ToString()),
                    new Claim(BankClaims.Id, user.Id.ToString())
                ];

                await manager.AddClaimsAsync(user, claims);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
