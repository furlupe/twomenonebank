using CreditEntity = Bank.Credit.Domain.Credit.Credit;

namespace Bank.Credit.Domain
{
    public class User : Entity
    {
        public List<CreditEntity> Credits { get; set; } = [];
        public int CreditRating { get; private set; } = 100;

        public void SetRating(int rating)
        {
            if (rating < 0 || rating > 100)
            {
                throw new InvalidDataException("Credit rating percent must be in range [0, 100]");
            }

            CreditRating = rating;
        }

        public void AddCredit(CreditEntity credit)
        {
            Credits.Add(credit);
        }

        private User() { }

        public User(Guid id)
        {
            Id = id;
        }
    }
}
