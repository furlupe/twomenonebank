using Bank.Common.DateTimeProvider;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;
using Bank.Core.Persistence;

namespace Bank.Core.App.Services;

public class TransactionService(CoreDbContext db, IDateTimeProvider dateProvider)
    : ITransactionService
{
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
