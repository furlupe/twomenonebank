using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Bank.Core.App.Dto;
using Bank.Core.App.Dto.Pagination;
using Bank.Core.App.Services.Contracts;
using Bank.Exceptions.WebApiException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Core.App.Controllers;

[Route("accounts")]
[Authorize(Policy = Policies.CreateUserIfNeeded)]
public class AccountsController(IAccountService accountService) : ControllerBase
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

        if (account.User.Id != User.GetId())
            throw new NotFoundException();

        return AccountDto.From(account);
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
