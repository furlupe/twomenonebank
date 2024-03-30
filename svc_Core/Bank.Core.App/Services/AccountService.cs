﻿using System.Security.Principal;
using Bank.Common.Pagination;
using Bank.Common.Utils;
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

    public async Task<Guid> CreateAccountFor(Guid id, AccountCreateDto dto)
    {
        var user = await userService.GetUser(id);
        if (await db.Accounts.AnyAsync(x => x.UserId == id && x.Name == dto.Name))
            throw new ConflictingChangesException($"Account with name {dto.Name} already exists.");

        user.OpenNewAccount(dto.Name, dto.Currency);
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
        await db.Accounts.AnyAsync(x => x.Id == accountId && x.UserId == userId);

    public async Task<Account> GetUserDefaultAccount(Guid userId)
    {
        var account = await db.Accounts.FirstOrDefaultAsync(x => x.UserId == userId);
        CheckDefaultAccount(account);
        return account!;
    }

    public void CheckDefaultAccount(Account? account) =>
        Validation.Check(
            ExceptionConstants.MsgInvalidAction,
            account != null,
            "Could not transfer: transferee does not have an account set for incoming transfers."
        );
}
