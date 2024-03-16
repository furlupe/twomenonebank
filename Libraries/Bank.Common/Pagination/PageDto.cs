namespace Bank.Common.Pagination;

/// <summary>
/// Contains pagination info and items associated with current page.
/// </summary>
/// <param name="Items">Items associated with the page.</param>
/// <param name="PageInfo"><inheritdoc cref="PageInfo"/></param>
public record PageDto<TItem>(List<TItem> Items, PageInfo PageInfo)
{
    public PageDto<TNewItem> Cast<TNewItem>(Func<TItem, TNewItem> mapper) =>
        new(Items.Select(mapper).ToList(), PageInfo);
}

/// <summary>
/// Contains information about pagination related to a request.
/// </summary>
/// <param name="CurrentPage">Current page's number.</param>
/// <param name="TotalItems">Number of items available that meet request requirements.</param>
/// <param name="TotalPages">Number of pages that can be served with available items.</param>
public record struct PageInfo(int CurrentPage, int TotalItems, int TotalPages);
