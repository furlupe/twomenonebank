package com.example.employeeclient.domain.model.account.event

import com.example.employeeclient.domain.model.enums.Currency

data class BalanceDomain(
    val amount: Int = 0,
    val currency: Currency = Currency.RUB
)
