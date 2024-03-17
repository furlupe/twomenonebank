package com.example.employeeclient.data.remote.dto.account.event

import com.example.employeeclient.data.remote.dto.account.event.BalanceChangeDto

data class TransferDto(
    val id: String,
    val source: BalanceChangeDto,
    val target: BalanceChangeDto
)