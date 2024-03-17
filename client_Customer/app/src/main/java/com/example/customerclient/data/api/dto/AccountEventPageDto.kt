package com.example.customerclient.data.api.dto

data class AccountEventPageDto(
    val items: List<AccountEventDto>?,
    val pageInfo: PageInfoDto
)
