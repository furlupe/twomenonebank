using Bank.Credit.App.Dto;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Bank.Credit.App.Services
{
    public class CreditUserService
    {
        private readonly BankCreditDbContext _bankCreditDbContext;

        public CreditUserService(BankCreditDbContext bankCreditDbContext)
        {
            _bankCreditDbContext = bankCreditDbContext;
        }

        public async Task<UserDto> GetUser(Guid id)
        {
            var user = await _bankCreditDbContext.Users.SingleAsync(x =>
                x.Id == id && !x.IsDeleted
            );
            return new() { Id = user.Id, CreditRating = user.CreditRating };
        }
    }
}
