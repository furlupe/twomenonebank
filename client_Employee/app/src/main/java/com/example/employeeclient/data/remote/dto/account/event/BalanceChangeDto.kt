package com.example.employeeclient.data.remote.dto.account.event

data class BalanceChangeDto(
    val value: Int,
    val accountId: String,
    val eventType: String,
    val creditPayment: CreditPaymentDto?
)