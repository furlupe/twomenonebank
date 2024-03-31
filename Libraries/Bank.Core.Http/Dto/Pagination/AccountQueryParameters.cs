using Bank.Common.Pagination;

namespace Bank.Core.Http.Dto.Pagination;

public class AccountQueryParameters : QueryParameters
{
    public string? Name { get; set; }
    public bool? ShowClosed { get; set; }
}
