package com.example.customerclient.data.api.dto

data class CreateCreditDto(
    val tariffId: String,
    val withdrawalAccountId: String,
    val destinationAccountId: String,
    val amount: Int,
    val days: Int
)