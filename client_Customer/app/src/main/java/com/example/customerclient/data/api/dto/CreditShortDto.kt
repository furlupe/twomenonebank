package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.home.CreditShortInfo

data class CreditShortDto(
    val id: String,
    val amount: Int,
    val tariff: String,
    val days: Int,
    val isClosed: Boolean
)

fun CreditShortDto.toCreditInfo() = CreditShortInfo(
    id = this.id,
    type = this.tariff,
    balance = "${this.amount} ₽",
    date = this.days.toString(),
    nextFee = "Количество дней",
    isClosed = this.isClosed
)
