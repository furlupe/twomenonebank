package com.example.employeeclient.data.remote.dto.account.event

data class TransferDto(
    val id: String,
    val source: BalanceChangeDto,
    val target: BalanceChangeDto
)