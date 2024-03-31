package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.home.BillInfo

data class AccountDto(
    val id: String,
    val balance: MoneyDto,
    val name: String?
)

fun AccountDto.toBillInfo() = BillInfo(
    id = this.id,
    name = this.name ?: "",
    currency = this.balance.currency,
    balance = "${this.balance.amount} ${this.balance.currency}",
    type = "Сберегательный счёт",
    duration = "бессрочный"
)
