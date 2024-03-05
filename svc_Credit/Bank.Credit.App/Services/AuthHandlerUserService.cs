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

        public Task<bool> Exists(Guid id) => _dbContext.Users.AnyAsync(x => x.Id == id);
    }
}
