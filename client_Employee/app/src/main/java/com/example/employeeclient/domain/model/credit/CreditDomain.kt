package com.example.employeeclient.domain.model.credit

data class CreditDomain(
    val id: String,
    val amount: Int = 0,
    val tariff: String = "",
    val days: Int = 0,
    val isClosed: Boolean = false
)