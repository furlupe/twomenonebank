using Bank.Common.Pagination;
using Bank.Core.Domain;
using Bank.Core.Http.Dto;
using Bank.Core.Http.Dto.Pagination;

namespace Bank.Core.App.Services.Contracts;

public interface IAccountService
{
    Task<Account> GetAccount(Guid id);
    Task<Account> GetMasterAccount();
    Task SetDefaultAccount(Guid userId, Guid accountId);
    Task<Account> GetUserDefaultAccount(Guid userId);
    Task<PageDto<Account>> GetAccountsFor(Guid id, AccountQueryParameters queryParameters);
    Task<Guid> OpenAccountFor(Guid id, AccountOpenDto dto, Guid idempotenceKey);
    Task CloseAccount(Guid id, Guid idempotenceKey);
    Task<bool> IsAccountOwnedBy(Guid accountId, Guid userId);
    Task<Account> GetAccountIfOwnedBy(Guid accountId, Guid userId);
    Task CheckAccountOwnedBy(Guid accountId, Guid userId);
}
