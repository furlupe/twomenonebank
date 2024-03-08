using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers.Credits
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class CreditController : ControllerBase
    {
        private readonly CreditService _creditService;

        public CreditController(CreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<IActionResult> Create([FromBody] CreateCreditDto dto)
        {
            await _creditService.Create(User.GetId(), dto);
            return Ok();
        }
    }
}
