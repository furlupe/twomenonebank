using System.Security.Claims;
using Bank.Auth.Common.Enumerations;
using Bank.Auth.Domain;
using Bank.Auth.Domain.Models;
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
                        Roles = [Role.Admin.ToString(), Role.Employee.ToString()],
                        PhoneNumber = "1234567890",
                    };

                await manager.CreateAsync(user);
                await manager.AddPasswordAsync(user, "balls");

                List<Claim> claims = [new Claim(Claims.Subject, user.Id.ToString()),];
                user.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

                await manager.AddClaimsAsync(user, claims);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
