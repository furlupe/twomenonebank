using System.ComponentModel.DataAnnotations;

namespace Bank.Common.Pagination;

/// <summary>
/// Base class for query parameters used by requests featuring pagination.
/// </summary>
public class QueryParameters
{
    /// <summary>
    /// Number of the page requested.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Desired number of items per page.
    /// </summary>
    [Range(1, 64)]
    public int PageSize { get; set; } = 16;

    public SortingType SortingType { get; set; } = SortingType.Descending;
}

public enum SortingType
{
    Ascending = 0,
    Descending = 1
}
