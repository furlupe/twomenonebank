using System.ComponentModel.DataAnnotations;
using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers.Credits
{
    public partial class CreditController
    {
        [HttpGet("user/{userId}")]
        [Authorize(Policy = Policies.EmployeeOrHigher)]

        public async Task<ActionResult<PageDto<CreditSmallDto>>> GetUserCredits(
            Guid userId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetUserCredits(userId, page));

        [HttpGet("{creditId}")]
        [Authorize(Policy = Policies.EmployeeOrHigher)]

        public async Task<ActionResult<CreditDto>> GetCreditDetails(Guid creditId) =>
            Ok(await _creditService.GetCredit(creditId));

        [HttpGet("{creditId}/operations")]
        [Authorize(Policy = Policies.EmployeeOrHigher)]

        public async Task<ActionResult<PageDto<CreditOperationDto>>> GetCreditOperations(
            Guid creditId,
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _creditService.GetCreditOperationHistory(creditId, page));
    }
}
