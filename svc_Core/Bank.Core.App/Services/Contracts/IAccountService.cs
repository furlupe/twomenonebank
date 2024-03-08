using Bank.Common.Pagination;
using Bank.Core.App.Dto;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.Domain;

namespace Bank.Core.App.Services.Contracts;

public interface IAccountService
{
    Task<Account> GetAccount(Guid id);
    Task<PageDto<Account>> GetAccountsFor(Guid id, AccountQueryParameters queryParameters);
    Task<Guid> CreateAccountFor(Guid id, AccountCreateDto dto);
    Task<bool> AccountOwnedBy(Guid accountId, Guid userId);
    Task CloseAccount(Guid id);
}
