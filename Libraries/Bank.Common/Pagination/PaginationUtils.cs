using System.Linq.Expressions;
using Bank.Exceptions.WebApiException;
using Microsoft.EntityFrameworkCore;

namespace Bank.Common.Pagination;

/// <summary>
/// Provides methods used to handle requests featuring pagination.
/// </summary>
public static class PaginationUtils
{
    public static async Task<PageDto<TResult>> GetPage<TSource, TResult>(
        this IQueryable<TSource> query,
        QueryParameters queryParameters,
        Func<TSource, TResult> mapper,
        Expression<Func<TSource, object>>? sortingAttributeAccessor = null
    )
    {
        var totalCount = await query.CountAsync();

        if (!PageExists(queryParameters, totalCount))
            throw new NotFoundException(MsgPageNotFound);

        if (sortingAttributeAccessor != null)
            query = query.SortBy(sortingAttributeAccessor, queryParameters.SortingType);

        var queryResult = await query.TakePage(queryParameters).ToListAsync();
        return queryResult.ToPage(mapper, queryParameters, totalCount);
    }

    /// <summary>
    /// Given request's <see cref="QueryParameters"/>, creates <see cref="PageDto{TItem}"/> from an enumerable of objects.
    /// </summary>
    /// <param name="mapper">A function used to transform objects contained in the source into the form they will be present in the paginated dto.</param>
    /// <param name="totalItems">The number of items matching the associated query.</param>
    /// <param name="source">Source which contains items to be handled.</param>
    /// <param name="queryParameters"><see cref="QueryParameters"/></param>
    public static PageDto<TResult> ToPage<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> mapper,
        QueryParameters queryParameters,
        int totalItems
    ) =>
        new(
            source.Select(x => mapper(x)).ToList(),
            new PageInfo(
                queryParameters.PageNumber,
                totalItems,
                CountPages(totalItems, queryParameters.PageSize)
            )
        );

    /// <summary>
    /// Selects items associated with a certain page.
    /// </summary>
    public static IQueryable<TSource> TakePage<TSource>(
        this IQueryable<TSource> query,
        QueryParameters queryParameters
    ) =>
        query
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);

    /// <summary>
    /// Given requested page's number, determines whether it exists, based on specified page size and the number of items matching the associated query.
    /// </summary>
    public static bool PageExists(QueryParameters queryParameters, int totalItems) =>
        (
            queryParameters.PageNumber >= 1
            && queryParameters.PageNumber <= CountPages(totalItems, queryParameters.PageSize)
        );

    /// <summary>
    /// Calculates number of available pages based on the number of items matching the associated query and the specified size of a page.
    /// </summary>
    public static int CountPages(int totalItems, int pageSize) =>
        Math.Max((totalItems + pageSize - 1) / pageSize, 1);

    public const string MsgPageNotFound = "Requested page does not exist.";
}
