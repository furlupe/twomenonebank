using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bank.Common.Pagination;

public static class QueryingUtils
{
    public static IOrderedQueryable<TModel> SortBy<TModel>(
        this IQueryable<TModel> query,
        Expression<Func<TModel, object>> accessor,
        SortingType sortingType
    ) =>
        sortingType switch
        {
            SortingType.Ascending => query.OrderBy(accessor),
            SortingType.Descending => query.OrderByDescending(accessor),
            _ => throw new ArgumentOutOfRangeException(nameof(sortingType))
        };

    public static IQueryable<TSource> FilterByString<TSource>(
        this IQueryable<TSource> queryable,
        Expression<Func<TSource, string>> accessor,
        string? value
    )
    {
        if (value == null)
            return queryable;

        Expression<Func<string, bool>> filterConditionExpression = x =>
            EF.Functions.Like(x.ToLower(), $"%{value.ToLower()}%");

        InvocationExpression filterInvocationExpression = Expression.Invoke(
            filterConditionExpression,
            accessor.Body
        );

        return queryable.Where(
            Expression.Lambda<Func<TSource, bool>>(filterInvocationExpression, accessor.Parameters)
        );
    }
}
