package com.example.employeeclient.data.remote.dto.credit

import com.example.employeeclient.data.remote.dto.tariff.TariffDto

data class CreditDetailsDto(
    val id: String,
    val tariff: TariffDto,
    val amount: Int,
    val baseAmount: Int,
    val days: Int,
    val penalty: Int,
    val periodicPayment: Int,
    val isClosed: Boolean
)