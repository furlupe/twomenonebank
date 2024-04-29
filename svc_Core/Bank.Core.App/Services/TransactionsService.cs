using Bank.Common.DateTimeProvider;
using Bank.Common.Pagination;
using Bank.Core.App.Hubs;
using Bank.Core.App.Services.Contracts;
using Bank.Core.App.Utils;
using Bank.Core.Domain.Events;
using Bank.Core.Http.Dto.Events;
using Bank.Core.Http.Dto.Pagination;
using Bank.Core.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Services;

public class TransactionsService(
    CoreDbContext db,
    ITransactionsFactory transactionFactory,
    IHubContext<TransactionsHub, ITransactionsClient> transactionsHub
) : ITransactionsService
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
        var completed = await db
            .Accounts.Where(x =>
                x.Id == model.SourceId
                && x.Events.Any(x => x.IdempotenceKey == model.IdempotenceKey)
            )
            .AnyAsync();
        if (completed)
            return;

        var transaction = await transactionFactory.Create(model);
        var @event = await transaction.Perform();
        await db.SaveChangesAsync();

        await transactionsHub
            .Clients.Groups(GetAffectedAccountIds(@event))
            .ReceiveTransactions([AccountEventDto.From(@event)]);
    }

    private static List<string> GetAffectedAccountIds(AccountEvent @event)
    {
        List<Guid?> accountIds =
        [
            @event.Transfer?.Target.AccountId,
            @event.Transfer?.Source.AccountId,
            @event.BalanceChange?.AccountId
        ];
        return accountIds.Where(x => x is not null).Cast<Guid>().Select(x => x.ToString()).ToList();
    }
}
