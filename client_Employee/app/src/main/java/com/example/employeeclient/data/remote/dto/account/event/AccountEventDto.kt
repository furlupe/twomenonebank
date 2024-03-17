package com.example.employeeclient.data.remote.dto.account.event

data class AccountEventDto(
    val id: String,
    val comment: String?,
    val eventType: String,
    val balanceChange: BalanceChangeDto?,
    val transfer: TransferDto?,
    val resolvedAt: String,
    val state: String
)