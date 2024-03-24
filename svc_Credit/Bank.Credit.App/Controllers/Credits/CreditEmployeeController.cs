using System.ComponentModel.DataAnnotations;
using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Enumerations;
using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers.Credits
{
    [Route("api/manage/credits")]
    [ApiController]
    [Authorize]
    [CalledByStaff]
    public class CreditEmployeeController : ControllerBase
    {
        private readonly CreditService _creditService;

        public CreditEmployeeController(CreditService creditService)
        {
            _creditService = creditService;
        }

        [HttpGet("of/{userId}")]
        public async Task<ActionResult<PageDto<CreditSmallDto>>> GetUserCredits(
            Guid userId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetUserCredits(userId, page));

        [HttpGet("{creditId}")]
        public async Task<ActionResult<CreditDto>> GetCreditDetails(Guid creditId) =>
            Ok(await _creditService.GetCredit(creditId));

        [HttpGet("{creditId}/operations")]
        public async Task<ActionResult<PageDto<CreditOperationDto>>> GetCreditOperations(
            Guid creditId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetCreditOperationHistory(creditId, page));
    }
}
