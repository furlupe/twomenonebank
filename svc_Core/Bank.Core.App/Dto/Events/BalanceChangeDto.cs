﻿using Bank.Core.Domain.Events;

namespace Bank.Core.App.Dto.Events;

public class BalanceChangeDto
{
    public long Value { get; protected set; }
    public Guid AccountId { get; protected set; }

    public BalanceChangeType EventType { get; protected set; }
    public CreditPaymentDto? CreditPayment { get; protected set; }

    public static BalanceChangeDto? From(BalanceChange? model) =>
        model == null
            ? null
            : new()
            {
                AccountId = model.AccountId,
                Value = model.Value,
                EventType = model.EventType,
                CreditPayment = CreditPaymentDto.From(model.CreditPayment),
            };
}
