package com.example.customerclient.data.api.dto

data class CreditOperationPageDto(
    val items: List<CreditOperationDto>?,
    val pageInfo: PageInfoDto
)
