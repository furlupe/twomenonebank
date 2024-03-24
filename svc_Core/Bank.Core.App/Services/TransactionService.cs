using Bank.Common.DateTimeProvider;
using Bank.Common.Money.Converter;
using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain;
using Bank.Core.Domain.Events;
using Bank.Core.Domain.Transactions;
using Bank.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class TransactionService(
    CoreDbContext db,
    IDateTimeProvider dateProvider,
    ICurrencyConverter converter
) : ITransactionService
{
    public async Task<PageDto<AccountEvent>> GetAccountTransactions(
        Guid id,
        TransactionQueryParameters queryParameters
    )
    {
        var query = db
            .Accounts.AsNoTrackingWithIdentityResolution()
            .Where(x => x.Id == id)
            .SelectMany(x => x.Events)
            .WhereResolvedAt(queryParameters);

        return await query.GetPage(queryParameters, x => x);
    }

    public async Task Deposit(Account source, Deposit transaction)
    {
        await source.Deposit(transaction, dateProvider.UtcNow, converter);
        await db.SaveChangesAsync();
    }

    public async Task Withdraw(Account source, Withdrawal transaction)
    {
        await source.Withdraw(transaction, dateProvider.UtcNow, converter);
        await db.SaveChangesAsync();
    }

    public async Task RepayCredit(Account source, Domain.Transactions.CreditPayment transaction)
    {
        await source.RepayCredit(transaction, dateProvider.UtcNow, converter);
        await db.SaveChangesAsync();
    }
}
