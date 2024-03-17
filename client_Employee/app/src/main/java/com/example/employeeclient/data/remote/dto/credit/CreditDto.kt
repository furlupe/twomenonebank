package com.example.employeeclient.data.remote.dto.credit

data class CreditDto(
    val id: String,
    val amount: Int,
    val tariff: String?,
    val days: Int,
    val isClosed: Boolean
)