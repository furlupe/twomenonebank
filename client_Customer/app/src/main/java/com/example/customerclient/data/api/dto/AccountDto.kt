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
    balance = this.balance.amount.toString(),
    type = "Сберегательный счёт",
    duration = "бессрочный"
)
