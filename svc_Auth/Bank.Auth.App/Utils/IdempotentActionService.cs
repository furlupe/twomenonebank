using Bank.Auth.Domain;
using Bank.Idempotency;
using Microsoft.EntityFrameworkCore;

namespace Bank.Auth.App.Utils
{
    public class IdempotentActionService : IActionService
    {
        private readonly BankAuthDbContext _dbContext;

        public IdempotentActionService(BankAuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task StoreAction(ActionDescriptor action)
        {
            await _dbContext.ActionDescriptors.AddAsync(action);
            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> WasActionExecuted(ActionDescriptor action) =>
            _dbContext.ActionDescriptors.AnyAsync(x =>
                x.Name == action.Name && x.IdempotencyKey == action.IdempotencyKey
            );
    }
}
