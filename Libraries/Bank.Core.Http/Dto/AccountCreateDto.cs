using System.ComponentModel.DataAnnotations;
using Bank.Common.Money;

namespace Bank.Core.Http.Dto;

public class AccountCreateDto
{
    [Required]
    [Length(3, 255)]
    public string Name { get; set; } = null!;

    [Required]
    public Currency Currency { get; set; }
}
