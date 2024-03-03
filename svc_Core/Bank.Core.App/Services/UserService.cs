using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain;
using Bank.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class UserService(CoreDbContext db) : IUserService
{
    public Task<User> GetUser(Guid id)
        => db.Users.SingleAsync(u => u.Id == id);

    public async Task EnsureUserExists(Guid id)
    {
        if (await db.Users.AnyAsync(u => u.Id == id))
            return;

        db.Users.Add(new User(id));
        await db.SaveChangesAsync();
    }
}
