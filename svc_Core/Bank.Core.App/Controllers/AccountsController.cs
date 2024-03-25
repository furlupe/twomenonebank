using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies;
using Bank.Common.Pagination;
using Bank.Core.App.Dto;
using Bank.Core.App.Dto.Events;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[ApiController]
[Authorize, CalledByUser]
public class AccountsController(
    IAccountService accountService,
    ITransactionService transactionService
) : ControllerBase
{
    [HttpGet("my")]
    public async Task<PageDto<AccountDto>> GetMyAccounts(
        [FromQuery] AccountQueryParameters queryParameters
    )
    {
        var accounts = await accountService.GetAccountsFor(User.GetId(), queryParameters);
        return accounts.Cast(AccountDto.From);
    }

    [HttpGet("{id}")]
    public async Task<AccountDto> GetAccount([FromRoute] Guid id)
    {
        var account = await accountService.GetAccount(id);

        if (account.UserId != User.GetId())
            throw new NotFoundException();

        return AccountDto.From(account);
    }

    [HttpGet("{id}/history")]
    public async Task<PageDto<AccountEventDto>> GetAccountOperations(
        [FromRoute] Guid id,
        [FromQuery] TransactionQueryParameters queryParameters
    )
    {
        if (!await accountService.IsAccountOwnedBy(id, User.GetId()))
            throw new NotFoundException();
        var transactions = await transactionService.GetAccountTransactions(id, queryParameters);

        return transactions.Cast(AccountEventDto.From);
    }

    [HttpPost("open")]
    public async Task<Guid> CreateAccount([FromBody] AccountCreateDto dto)
    {
        return await accountService.CreateAccountFor(User.GetId(), dto);
    }

    [HttpDelete("close/{id}")]
    public async Task CloseAccount([FromRoute] Guid id)
    {
        if (!await accountService.IsAccountOwnedBy(id, User.GetId()))
            throw new NotFoundException();

        await accountService.CloseAccount(id);
    }
}
