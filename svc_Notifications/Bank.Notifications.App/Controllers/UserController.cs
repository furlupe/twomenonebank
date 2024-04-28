using Bank.Auth.Common.Attributes;
using Bank.Auth.Common.Extensions;
using Bank.Notifications.App.Dto;
using Bank.Notifications.Domain;
using Bank.Notifications.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Notifications.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, CalledByHuman]
    public class UserController : ControllerBase
    {
        private readonly BankNotificationsDbContext _dbContext;

        public UserController(BankNotificationsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// For clients use: creates user w/ given device token for futher notification delivery
        /// </summary>

        [HttpPost]
        public async Task UpsertUser([FromBody] UserDto dto)
        {
            var userId = HttpContext.User.GetId();
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == userId);
            
            if (user != null)
            {
                await _dbContext.Users.ExecuteUpdateAsync(x => x.SetProperty(u => u.Token, dto.Token));
                return;
            }

            await _dbContext.Users.AddAsync(new User() { Id = userId, Token = dto.Token });
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
