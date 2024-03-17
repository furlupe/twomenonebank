using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Bank.Core.App.Dto;
using Bank.Core.App.Dto.Events;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("manage/accounts")]
[Authorize(Policy = Policies.EmployeeOrHigher)]
public class AccountsEmployeeController(
    IUserService userService,
    IAccountService accountService,
    ITransactionService transactionService
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<AccountDto> GetAccount([FromRoute] Guid id)
    {
        var account = await accountService.GetAccount(id);
        return AccountDto.From(account);
    }

    [HttpGet("of/{id}")]
    public async Task<PageDto<AccountDto>> GetUserAccounts(
        [FromRoute] Guid id,
        [FromQuery] AccountQueryParameters queryParameters
    )
    {
        var accounts = await accountService.GetAccountsFor(id, queryParameters);
        return accounts.Cast(AccountDto.From);
    }

    [HttpGet("{id}/history")]
    public async Task<PageDto<AccountEventDto>> GetAccountOperations(
        [FromRoute] Guid id,
        TransactionQueryParameters queryParameters
    )
    {
        var transactions = await transactionService.GetAccountTransactions(id, queryParameters);

        return transactions.Cast(AccountEventDto.From);
    }
}
