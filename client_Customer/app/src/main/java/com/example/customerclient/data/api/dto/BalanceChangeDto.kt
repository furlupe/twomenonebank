package com.example.customerclient.data.api.dto

data class BalanceChangeDto(
    val nativeValue: MoneyDto,
    val foreignValue: MoneyDto,
    val accountId: String,
    val eventType: String?,
    val creditPayment: CreditPaymentDto?
)
