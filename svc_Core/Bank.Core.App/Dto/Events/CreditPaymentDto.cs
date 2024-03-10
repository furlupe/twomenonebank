using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto.Events;

public class CreditPaymentDto
{
    public Guid CreditId { get; set; }

    public static CreditPaymentDto? From(CreditPayment? model) =>
        model == null ? null : new() { CreditId = model.CreditId };
}
