package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.credit.info.CreditHistory

data class CreditOperationDto(
    val id: String,
    val creditId: String,
    val type: Int,
    val happenedAt: String,
    val to: String?,
    val amount: Int?
)

fun CreditOperationDto.toCreditHistory() = CreditHistory(
    id = this.id,
    type = this.type,
    date = this.happenedAt.split("T")[0],
    to = this.to,
    amount = amount?.toString() ?: ""
)
