using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Enumerations;
using Bank.Common.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Http.Dto;
using Bank.Core.Http.Dto.Events;
using Bank.Core.Http.Dto.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("manage/accounts")]
[ApiController]
[Authorize]
public class AccountsEmployeeController(
    IAccountService accountService,
    ITransactionsService transactionService
) : ControllerBase
{
    [HttpGet("{id}")]
    [CalledByStaff]
    public async Task<AccountDto> GetAccount([FromRoute] Guid id)
    {
        var account = await accountService.GetAccount(id);
        return AccountDto.From(account);
    }

    [HttpGet("of/{id}")]
    [CalledByStaff]
    public async Task<PageDto<AccountDto>> GetUserAccounts(
        [FromRoute] Guid id,
        [FromQuery] AccountQueryParameters queryParameters
    )
    {
        var accounts = await accountService.GetAccountsFor(id, queryParameters);
        return accounts.Cast(AccountDto.From);
    }

    [HttpGet("{id}/history")]
    [CalledByStaff]
    public async Task<PageDto<AccountEventDto>> GetAccountOperations(
        [FromRoute] Guid id,
        [FromQuery] TransactionQueryParameters queryParameters
    )
    {
        var transactions = await transactionService.GetAccountTransactions(id, queryParameters);

        return transactions.Cast(AccountEventDto.From);
    }

    [HttpGet("master")]
    [CalledBy(Caller.Human | Caller.Service, Role.Employee)]
    public async Task<AccountDto> GetMasterAccount()
    {
        var account = await accountService.GetMasterAccount();
        return AccountDto.From(account);
    }

    [HttpGet("master/history")]
    [CalledBy(Caller.Human | Caller.Service, Role.Employee)]
    public async Task<PageDto<AccountEventDto>> GetMasterAccountOperations(
        [FromQuery] TransactionQueryParameters queryParameters
    )
    {
        var transactions = await transactionService.GetMasterAccountTransactions(queryParameters);

        return transactions.Cast(AccountEventDto.From);
    }
}
