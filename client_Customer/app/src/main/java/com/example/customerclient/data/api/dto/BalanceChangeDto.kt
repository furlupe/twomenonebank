package com.example.customerclient.data.api.dto

data class BalanceChangeDto(
    val value: Int,
    val accountId: String,
    val eventType: String,
    val creditPayment: CreditPaymentDto?
)
