package com.example.employeeclient.data.remote.dto.account.event

import com.example.employeeclient.data.remote.dto.account.BalanceDto

data class BalanceChangeDto(
    val nativeValue: BalanceDto,
    val foreignValue: BalanceDto,
    val accountId: String,
    val eventType: String?
)