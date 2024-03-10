using Bank.Common.Pagination;

namespace Bank.Core.App.Dto.Pagination;

public class AccountQueryParameters : QueryParameters
{
    public string? Name { get; set; }
}
