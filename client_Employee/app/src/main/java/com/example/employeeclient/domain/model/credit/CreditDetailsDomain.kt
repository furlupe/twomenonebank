package com.example.employeeclient.domain.model.credit

import com.example.employeeclient.domain.model.tariff.TariffDomain

data class CreditDetailsDomain(
    val id: String,
    val tariff: TariffDomain,
    val amount: Int,
    val baseAmount: Int,
    val days: Int,
    val penalty: Int,
    val periodicPayment: Int,
    val isClosed: Boolean
)