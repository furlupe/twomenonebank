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

        [HttpPost("{creditId}/pay")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<IActionResult> Pay(Guid creditId, [FromBody] CreditPaymentDto dto)
        {
            await _creditService.Pay(User.GetId(), creditId, dto.Amount);
            return Ok();
        }

        [HttpPost("{creditId}/pay-penalty")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<IActionResult> PayPenalty(Guid creditId, [FromBody] CreditPaymentDto dto)
        {
            await _creditService.PayPenalty(User.GetId(), creditId, dto.Amount);
            return Ok();
        }

    }
}
