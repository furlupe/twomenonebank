using Bank.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bank.Core.App.Utils;

public static class AccountQueryUtils
{
    public static Task<bool> HasIdempotentEventAsync(
        this IQueryable<Account> accounts,
        Guid idempotenceKey
    ) => accounts.Where(x => x.Events.Any(x => x.IdempotenceKey == idempotenceKey)).AnyAsync();
}
