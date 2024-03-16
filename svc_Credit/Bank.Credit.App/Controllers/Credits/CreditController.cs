using Bank.Auth.Shared.Extensions;
using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> Pay(Guid creditId)
        {
            await _creditService.Pay(User.GetId(), creditId);
            return Ok();
        }

        [HttpPost("{creditId}/pay-penalty")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<IActionResult> PayPenalty(Guid creditId)
        {
            await _creditService.PayPenalty(User.GetId(), creditId);
            return Ok();
        }

        [HttpGet("my")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<ActionResult<PageDto<CreditSmallDto>>> GetCurrentUserCredits(
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetUserCredits(User.GetId(), page));

        [HttpGet("my/{creditId}")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<ActionResult<CreditDto>> GetCurrentUserCreditDetails(Guid creditId) =>
            Ok(await _creditService.GetCredit(creditId, User.GetId()));

        [HttpGet("my/{creditId}/operations")]
        [Authorize(Policy = Policies.CreateUserIfNeeded)]
        public async Task<ActionResult<PageDto<CreditOperationDto>>> GetCurrentUserCreditOperations(
            Guid creditId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetCreditOperationHistory(creditId, page, User.GetId()));


    }
}
