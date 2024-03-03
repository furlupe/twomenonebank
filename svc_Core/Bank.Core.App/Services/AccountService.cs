using Bank.Core.App.Services.Contracts;
using Bank.Core.Domain;
using Bank.Core.Persistence;
using Bank.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class AccountService(CoreDbContext db) : IAccountService
{
    public async Task<Account> GetAccount(Guid id)
    {
        var account = await db.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        if (account is null) throw NotFoundException.ForModel<Account>(id);

        return account;
    }

    public async Task<Guid> CreateAccountFor(User user)
    {
        var account = new Account(user);
        await db.SaveChangesAsync();

        return account.Id;
    }

    public async Task<List<Account>> GetAccountsFor(User user)
    {
        var accounts = await db.Accounts.AsNoTracking().Where(x => x.User.Id == user.Id).ToListAsync();

        return accounts;
    }
}
