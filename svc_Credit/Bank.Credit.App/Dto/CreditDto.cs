namespace Bank.Credit.App.Dto
{
    public class CreateCreditDto
    {
        public Guid TariffId { get; set; }
        public int Amount { get; set; }
        public int Days { get; set; }
    }
}
