using System.Linq.Expressions;
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
}
