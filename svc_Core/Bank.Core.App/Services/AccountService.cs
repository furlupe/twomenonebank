using Bank.Common.Pagination;
using Bank.Core.App.Dto;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain;
using Bank.Core.Persistence;
using Bank.Exceptions.WebApiException;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class AccountService(CoreDbContext db, IUserService userService) : IAccountService
{
    public async Task<Account> GetAccount(Guid id)
    {
        var account = await db.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        if (account is null)
            throw NotFoundException.ForModel<Account>(id);

        return account;
    }

    public async Task<PageDto<Account>> GetAccountsFor(
        Guid id,
        AccountQueryParameters queryParameters
    )
    {
        var accounts = await db
            .Accounts.AsNoTrackingWithIdentityResolution()
            .FilterByString(x => x.Name, queryParameters.Name)
            .Where(x => x.User.Id == id)
            .GetPage(queryParameters, x => x);

        return accounts;
    }

    public async Task<Guid> CreateAccountFor(Guid id, AccountCreateDto dto)
    {
        var user = await userService.GetUser(id);
        if (await db.Accounts.AnyAsync(x => x.UserId == id && x.Name == dto.Name))
            throw new ConflictingChangesException($"Account with name {dto.Name} already exists.");

        user.OpenNewAccount(dto.Name);
        await db.SaveChangesAsync();

        return user.Accounts.Single(x => x.Name == dto.Name).Id;
    }

    public async Task CloseAccount(Guid id)
    {
        var account = await db.Accounts.SingleOrThrowAsync(x => x.Id == id);
        account.ValidateClose();

        db.Remove(account);
        await db.SaveChangesAsync();
    }

    public async Task<bool> IsAccountOwnedBy(Guid accountId, Guid userId) =>
        await db.Accounts.AnyAsync(x => x.Id == accountId && x.User.Id == userId);
}
