using Bank.Auth.Shared.Policies.Handlers;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class AuthHandlerUserService : IUserService
    {
        private readonly BankCreditDbContext _dbContext;

        public AuthHandlerUserService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Guid id)
        {
            await _dbContext.Users.AddAsync(new Domain.User(id));
            await _dbContext.SaveChangesAsync();
        }

        public async Task EnsureUserExists(Guid id)
        {
            var exists = await _dbContext.Users.AnyAsync(x => x.Id == id);
            if (exists)
                return;

            await _dbContext.Users.AddAsync(new Domain.User(id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
