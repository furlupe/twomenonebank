package com.example.customerclient.data.api.dto

data class TransferDto(
    val id: String,
    val source: BalanceChangeDto,
    val target: BalanceChangeDto
)
