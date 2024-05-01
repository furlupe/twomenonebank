using System.Linq.Expressions;
using Bank.Core.Domain.Events;
using Bank.Core.Http.Dto.Pagination;
using Bank.Exceptions.WebApiException;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Utils;

public static class QueryUtils
{
    public static async Task<TSource> SingleOrThrowAsync<TSource>(
        this IQueryable<TSource> queryable,
        Expression<Func<TSource, bool>> predicate
    ) =>
        await queryable.SingleOrDefaultAsync(predicate)
        ?? throw NotFoundException.ForModel<TSource>();

    public static IQueryable<TransactionEvent> WhereResolvedAt(
        this IQueryable<TransactionEvent> query,
        TransactionQueryParameters parameters
    )
    {
        if (parameters.From is not null)
            query.Where(x => x.ResolvedAt >= parameters.From);
        if (parameters.To is not null)
            query.Where(x => x.ResolvedAt <= parameters.To);

        return query;
    }
}
