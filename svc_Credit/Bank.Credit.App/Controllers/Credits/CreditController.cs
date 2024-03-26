using System.ComponentModel.DataAnnotations;
using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Auth.Common.Policies;
using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Bank.Credit.Domain.Credit.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers.Credits
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.CreateUserIfNeeded)]
    [CalledByUser]
    [ApiController]
    public partial class CreditController : ControllerBase
    {
        private readonly CreditService _creditService;

        public CreditController(CreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCreditDto dto)
        {
            await _creditService.Create(User.GetId(), dto);
            return Ok();
        }

        [HttpPost("{creditId}/pay")]
        public async Task<IActionResult> Pay(Guid creditId)
        {
            await _creditService.Pay(User.GetId(), creditId);
            return Ok();
        }

        [HttpPost("{creditId}/pay-penalty")]
        public async Task<IActionResult> PayPenalty(Guid creditId)
        {
            await _creditService.PayPenalty(User.GetId(), creditId);
            return Ok();
        }

        [HttpGet("my")]
        public async Task<ActionResult<PageDto<CreditSmallDto>>> GetCurrentUserCredits(
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetUserCredits(User.GetId(), page));

        [HttpGet("my/{creditId}")]
        public async Task<ActionResult<CreditDto>> GetCurrentUserCreditDetails(Guid creditId) =>
            Ok(await _creditService.GetCredit(creditId, User.GetId()));

        [HttpGet("my/{creditId}/operations")]
        public async Task<ActionResult<PageDto<CreditOperationDto>>> GetCurrentUserCreditOperations(
            Guid creditId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1,
            [FromQuery] List<CreditEventType>? types = null
        ) =>
            Ok(
                await _creditService.GetCreditOperationHistory(
                    creditId,
                    page,
                    ofUser: User.GetId(),
                    ofTypes: types
                )
            );
    }
}
