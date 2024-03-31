using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies;
using Bank.Common.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Core.Http.Dto;
using Bank.Core.Http.Dto.Events;
using Bank.Core.Http.Dto.Pagination;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize(Policy = Policies.CreateUserIfNeeded), CalledByUser]
public class AccountsController(
    IAccountService accountService,
    ITransactionService transactionService
) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<AccountDto> GetAccount([FromRoute] Guid id) =>
        AccountDto.From(await accountService.GetAccountIfOwnedBy(id, User.GetId()));

    [HttpGet("my")]
    public async Task<PageDto<AccountDto>> GetMyAccounts(
        [FromQuery] AccountQueryParameters queryParameters
    )
    {
        var accounts = await accountService.GetAccountsFor(User.GetId(), queryParameters);
        return accounts.Cast(AccountDto.From);
    }

    [HttpGet("{id}/history")]
    public async Task<PageDto<AccountEventDto>> GetAccountOperations(
        [FromRoute] Guid id,
        [FromQuery] TransactionQueryParameters queryParameters
    )
    {
        await accountService.CheckAccountOwnedBy(id, User.GetId());
        var transactions = await transactionService.GetAccountTransactions(id, queryParameters);

        return transactions.Cast((x) => AccountEventDto.From(x));
    }

    [HttpPost("open")]
    public async Task<Guid> CreateAccount([FromBody] AccountCreateDto dto)
    {
        return await accountService.CreateAccountFor(User.GetId(), dto);
    }

    [HttpPost("{id}/set-default")]
    public async Task SetDefault([FromRoute] Guid id)
    {
        await accountService.SetDefaultAccount(User.GetId(), id);
    }

    [HttpDelete("close/{id}")]
    public async Task CloseAccount([FromRoute] Guid id)
    {
        if (!await accountService.IsAccountOwnedBy(id, User.GetId()))
            throw new NotFoundException();

        await accountService.CloseAccount(id);
    }
}
