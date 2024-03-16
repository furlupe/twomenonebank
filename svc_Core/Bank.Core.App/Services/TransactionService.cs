using Bank.Common.DateTimeProvider;
using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;
using Bank.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class TransactionService(CoreDbContext db, IDateTimeProvider dateProvider)
    : ITransactionService
{
    public async Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    )
    {
        var transactions = await db
            .Accounts.AsNoTrackingWithIdentityResolution()
            .Where(x => x.Id == id)
            .SelectMany(x => x.Events)
            .GetPage(queryParameters, x => x);

        return transactions;
    }

    public async Task Deposit(Guid accountId, Deposit transaction)
    {
        var account = await db.Accounts.SingleOrThrowAsync(x => x.Id == accountId);
        account.Deposit(transaction, dateProvider.UtcNow);
        await db.SaveChangesAsync();
    }

    public async Task Withdraw(Guid accountId, Withdraw transaction)
    {
        var account = await db.Accounts.SingleOrThrowAsync(x => x.Id == accountId);
        account.Withdraw(transaction, dateProvider.UtcNow);
        await db.SaveChangesAsync();
    }

    public async Task RepayCredit(Guid accountId, RepayCredit transaction)
    {
        var account = await db.Accounts.SingleOrThrowAsync(x => x.Id == accountId);
        account.RepayCredit(transaction, dateProvider.UtcNow);
        await db.SaveChangesAsync();
    }
}
