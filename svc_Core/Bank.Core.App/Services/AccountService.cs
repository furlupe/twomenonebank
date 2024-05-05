using Bank.Common.DateTimeProvider;
using Bank.Common.Pagination;
using Bank.Common.Utils;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain;
using Bank.Core.Http.Dto;
using Bank.Core.Http.Dto.Pagination;
using Bank.Core.Persistence;
using Bank.Exceptions.WebApiException;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class AccountService(
    CoreDbContext db,
    IUserService userService,
    IDateTimeProvider timeProvider
) : IAccountService
{
    public Task<Account> GetAccount(Guid id) => db.Accounts.SingleOrThrowAsync(x => x.Id == id);

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

    public async Task<Guid> OpenAccountFor(Guid id, AccountOpenDto dto, Guid idempotenceKey)
    {
        var existingAccount = await GetByOwnerAndNameIfExists(id, dto.Name);
        if (existingAccount is not null)
        {
            if (await AccountHasIdempotentEvent(existingAccount.Id, idempotenceKey))
                return existingAccount.Id;

            throw new ConflictingChangesException($"Account with name {dto.Name} already exists.");
        }

        var user = await userService.GetUser(id);
        user.OpenNewAccount(dto.Name, dto.Currency, timeProvider.UtcNow, idempotenceKey);
        await db.SaveChangesAsync();

        return user.Accounts.Single(x => x.Name == dto.Name).Id;
    }

    public async Task CloseAccount(Guid id, Guid idempotenceKey)
    {
        if (await AccountHasIdempotentEvent(id, idempotenceKey))
            return;

        var account = await db.Accounts.SingleOrThrowAsync(Account.HasId(id));
        account.Close(timeProvider.UtcNow, idempotenceKey);
        await db.SaveChangesAsync();
    }

    public async Task SetDefaultAccount(Guid userId, Guid accountId)
    {
        var user = await userService.GetUser(userId);
        var account = await GetAccountIfOwnedBy(accountId, userId);
        user.SetDefaultTransferAccount(account);
        await db.SaveChangesAsync();
    }

    public async Task<bool> IsAccountOwnedBy(Guid accountId, Guid userId) =>
        await db.Accounts.AnyAsync(x => x.Id == accountId && x.OwnerId == userId);

    public async Task<Account> GetAccountIfOwnedBy(Guid accountId, Guid userId)
    {
        var account = await GetAccount(accountId);
        if (account.OwnerId != userId)
            throw NotFoundException.ForModel<Account>();
        return account;
    }

    public async Task CheckAccountOwnedBy(Guid accountId, Guid userId)
    {
        if (!await db.Accounts.AnyAsync(x => x.Id == accountId && x.OwnerId == userId))
            throw NotFoundException.ForModel<Account>();
    }

    public async Task<Account> GetUserDefaultAccount(Guid userId)
    {
        var account = await db.Accounts.SingleOrDefaultAsync(x =>
            x.OwnerId == userId && x.User.DefaultTransferAccountId == x.Id
        );
        CheckDefaultAccount(account);
        return account!;
    }

    public void CheckDefaultAccount(Account? account) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            account != null,
            "Could not transfer: transferee does not have an account set for incoming transfers."
        );

    public Task<Account> GetMasterAccount() => db.Accounts.SingleOrThrowAsync(x => x.IsMaster);

    private Task<Account?> GetByOwnerAndNameIfExists(Guid ownerId, string name) =>
        db.Accounts.SingleOrDefaultAsync(Account.HasOwnerAndName(ownerId, name));

    private Task<bool> AccountHasIdempotentEvent(Guid accountId, Guid idempotenceKey) =>
        db.Accounts.Where(Account.HasId(accountId)).HasIdempotentEventAsync(idempotenceKey);
}
