package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.bottombar.home.BillInfo

data class AccountDto(
    val id: String,
    val balance: Int,
    val name: String?
)

fun AccountDto.toBillInfo() = BillInfo(
    id = this.id,
    name = this.name ?: "",
    balance = this.balance.toString(),
    type = "Сберегательный счёт",
    duration = "бессрочный"
)
