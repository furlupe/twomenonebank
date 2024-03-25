using Bank.Auth.Common.Policies.Handlers;

namespace Bank.TransactionsGateway.App.Services;

public class UserService() : IUserService
{
    public async Task EnsureUserExists(Guid id)
    {
        Console.WriteLine("Ensuring...");
    }
}
