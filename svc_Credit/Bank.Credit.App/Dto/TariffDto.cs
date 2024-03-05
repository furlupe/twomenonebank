using System.ComponentModel.DataAnnotations;

namespace Bank.Credit.App.Dto
{
    public class TariffDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
    }

    public class CreateTariffDto
    {
        [Required, MinLength(1)]
        public string Name { get; set; }

        [Required, Range(1, 100)]
        public int Rate { get; set; }
    }
}
