package com.example.employeeclient.data.remote.dto.account

data class AccountDto(
    val id: String,
    val balance: BalanceDto,
    val name: String?
)
