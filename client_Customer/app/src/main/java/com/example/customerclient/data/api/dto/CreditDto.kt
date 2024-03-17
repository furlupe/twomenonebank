package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.credit.info.CreditInfo

data class CreditDto(
    val id: String,
    val tariff: TariffDto,
    val amount: Int,
    val baseAmount: Int,
    val days: Int,
    val penalty: Int
)

fun CreditDto.toCreditInfo() = CreditInfo(
    id = this.id,
    date = this.days.toString(),
    amount = this.amount.toString(),
    penalty =this.penalty
)
