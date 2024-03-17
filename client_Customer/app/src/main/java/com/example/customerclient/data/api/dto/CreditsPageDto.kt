package com.example.customerclient.data.api.dto

data class CreditsPageDto(
    val items: List<CreditShortDto>,
    val pageInfo: PageInfoDto
)
