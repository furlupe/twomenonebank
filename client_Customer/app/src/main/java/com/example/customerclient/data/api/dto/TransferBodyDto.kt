package com.example.customerclient.data.api.dto

data class TransferBodyDto(
    val value: MoneyDto,
    val message: String?,
)
