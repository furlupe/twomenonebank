namespace Bank.Credit.Domain
{
    public class User : Entity
    {
        private User() { }

        public User(Guid id)
        {
            Id = id;
        }
    }
}
