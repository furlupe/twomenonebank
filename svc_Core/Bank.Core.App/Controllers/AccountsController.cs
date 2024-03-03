using Bank.Core.App.Controllers.Utils;
using Bank.Core.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bank.Core.App.Controllers;

[Route("[controller]")]
[Authorize]
public class AccountsController(UserService userService, AccountService accountService) : ControllerBase
{
    [HttpGet]
    public async Task<Guid> GetAccounts()
    {
        var user = await userService.GetUser(User.GetId());
        return await accountService.CreateAccountFor(user);
    }

    [HttpGet("create")]
    public async Task<Guid> CreateAccount()
    {
        var user = await userService.GetUser(User.GetId());
        return await accountService.CreateAccountFor(user);
    }
}
