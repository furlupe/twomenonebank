using Bank.Core.Domain;

namespace Bank.Core.App.Services.Contracts;

public interface IUserService : Auth.Common.Policies.Handlers.IUserService
{
    Task<User> GetUser(Guid id);
    Task<User?> GetUserIfExists(Guid id);
}
