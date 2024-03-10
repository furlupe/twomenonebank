using System.ComponentModel.DataAnnotations;
using Bank.Auth.Shared.Policies;
using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Credit.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TariffController : ControllerBase
    {
        private readonly TariffService _tariffService;

        public TariffController(TariffService tariffService)
        {
            _tariffService = tariffService;
        }

        [HttpGet]
        public async Task<ActionResult<PageDto<TariffDto>>> GetTariffs(
            [FromQuery, Range(1, int.MaxValue)] int page = 1
        ) => Ok(await _tariffService.GetTariffs(page));

        [HttpPost]
        [Authorize(Policy = Policies.EmployeeOrHigher)]
        public async Task<IActionResult> Create([FromBody] CreateTariffDto dto)
        {
            await _tariffService.CreateTariff(dto);
            return Ok();
        }
    }
}
