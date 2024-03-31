using Bank.Common.Pagination;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class TransactionService(CoreDbContext db, ITransactionFactory transactionFactory)
    : ITransactionService
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

        return await query.GetPage(queryParameters, x => x, x => x.ResolvedAt);
    }

    public async Task<PageDto<AccountEvent>> GetMasterAccountTransactions(
        TransactionQueryParameters queryParameters
    )
    {
        var query = db
            .Accounts.AsNoTrackingWithIdentityResolution()
            .Where(x => x.IsMaster)
            .SelectMany(x => x.Events)
            .WhereResolvedAt(queryParameters);

        return await query.GetPage(queryParameters, x => x, x => x.ResolvedAt);
    }

    public async Task Perform(Common.Transaction model)
    {
        var transaction = await transactionFactory.Create(model);
        await transaction.Perform();
        await db.SaveChangesAsync();
    }
}
