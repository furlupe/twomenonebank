using Bank.Common.Pagination;

namespace Bank.Core.Http.Dto.Pagination;

public class TransactionQueryParameters : QueryParameters
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}
