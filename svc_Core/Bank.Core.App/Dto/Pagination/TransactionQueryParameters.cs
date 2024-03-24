using Bank.Common.Pagination;

namespace Bank.Core.App.Dto.Pagination;

public class TransactionQueryParameters : QueryParameters
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
