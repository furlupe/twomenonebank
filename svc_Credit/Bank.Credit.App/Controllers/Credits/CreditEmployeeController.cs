using System.ComponentModel.DataAnnotations;
using Bank.Auth.Shared.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers.Credits
{
    [Authorize(Policy = Policies.EmployeeOrHigher)]
    public partial class CreditController
    {
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCredits(
            Guid userId,
            [FromQuery, Range(1, int.MaxValue)] int page
        ) => Ok(await _creditService.GetUserCredits(userId, page));

        [HttpGet("{creditId}")]
        public async Task<IActionResult> GetCreditDetails(Guid creditId) =>
            Ok(await _creditService.GetCredit(creditId));

        [HttpGet("{creditId}/operations")]
        public async Task<IActionResult> GetCreditOperations(
            Guid creditId,
            [FromQuery, Range(1, int.MaxValue)] int page
        ) => Ok(await _creditService.GetCreditOperationHistory(creditId, page));
    }
}
