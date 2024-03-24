using Bank.Common.Pagination;
using Bank.Credit.App.Dto;
using Bank.Credit.Domain.Credit.Events;
using Bank.Credit.Persistance;
using Microsoft.EntityFrameworkCore;
using Credits = Bank.Credit.Domain.Credit;

namespace Bank.Credit.App.Services
{
    public class CreditService
    {
        private readonly BankCreditDbContext _dbContext;

        public CreditService(BankCreditDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Guid userId, CreateCreditDto dto)
        {
            var user = await _dbContext.Users.SingleAsync(x => x.Id == userId);
            var tariff = await _dbContext.Tariffs.SingleAsync(x => x.Id == dto.TariffId);

            await _dbContext.Credits.AddAsync(
                new Credits.Credit(user, tariff, dto.Amount, dto.Days)
            );
            await _dbContext.SaveChangesAsync();
        }

        public async Task Pay(Guid userId, Guid creditId)
        {
            var credit = await _dbContext
                .Credits.Include(x => x.Tariff)
                .SingleAsync(x => x.User.Id == userId && x.Id == creditId);
            credit.Pay();

            await _dbContext.SaveChangesAsync();
        }

        public async Task PayPenalty(Guid userId, Guid creditId)
        {
            var credit = await _dbContext
                .Credits.Include(x => x.Tariff)
                .SingleAsync(x => x.User.Id == userId && x.Id == creditId);
            credit.PayPenalty();

            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreditDto> GetCredit(Guid creditId, Guid? userId = null)
        {
            var credit = await _dbContext
                .Credits.Include(x => x.Tariff)
                .Include(x => x.User)
                .SingleAsync(x => x.Id == creditId);

            if (userId != null && credit.User.Id != userId)
            {
                throw new InvalidOperationException(
                    $"User {userId} is not the owner of credit {creditId}"
                );
            }

            return new()
            {
                Id = credit.Id,
                Tariff = new()
                {
                    Id = credit.Tariff.Id,
                    Name = credit.Tariff.Name,
                    Rate = credit.Tariff.Rate
                },
                Amount = credit.Amount,
                BaseAmount = credit.BaseAmount,
                Days = credit.Days,
                Penalty = credit.Penalty,
                PeriodicPayment = credit.PeriodicPayment,
                IsClosed = credit.IsClosed
            };
        }

        public Task<PageDto<CreditSmallDto>> GetUserCredits(Guid userId, int page) =>
            _dbContext
                .Credits.Where(x => x.User.Id == userId)
                .Include(credit => credit.Tariff)
                .GetPage(
                    new() { PageNumber = page },
                    credit => new CreditSmallDto()
                    {
                        Id = credit.Id,
                        Tariff = credit.Tariff.Name,
                        Amount = credit.Amount,
                        Days = credit.Days,
                        IsClosed = credit.IsClosed
                    }
                );

        public async Task<PageDto<CreditOperationDto>> GetCreditOperationHistory(
            Guid creditId,
            int page,
            Guid? userId = null
        )
        {
            var credit = await _dbContext.Credits.SingleAsync(x =>
                x.Id == creditId && (userId == null || x.User.Id == userId)
            );

            IQueryable<CreditEvent> query = _dbContext.Set<CreditEvent>();

            return await query
                .Where(x => x.AggregateId == credit.Id)
                .GetPage(new() { PageNumber = page }, FormEventDtoFromEventEntity);
        }

        private static CreditOperationDto FormEventDtoFromEventEntity(CreditEvent @event)
        {
            CreditOperationDto dto =
                new()
                {
                    Id = @event.Id,
                    CreditId = @event.AggregateId,
                    HappenedAt = @event.CreatedAt,
                    Type = @event.Type
                };

            switch (@event)
            {
                case CreditPaymentDateMovedEvent creditPaymentDateMovedEvent:
                    dto.To = creditPaymentDateMovedEvent.To;
                    break;
                case CreditPaymentMadeEvent creditPaymentMadeEvent:
                    dto.Amount = creditPaymentMadeEvent.Amount;
                    break;
                case CreditPenaltyAddedEvent creditPenaltyAddedEvent:
                    dto.Amount = creditPenaltyAddedEvent.Amount;
                    break;
                case CreditPenaltyPaidEvent creditPenaltyPaidEvent:
                    dto.Amount = creditPenaltyPaidEvent.Amount;
                    break;
                default:
                    break;
            }

            return dto;
        }
    }
}
