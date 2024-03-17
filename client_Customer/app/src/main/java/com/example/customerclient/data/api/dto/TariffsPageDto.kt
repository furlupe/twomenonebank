package com.example.customerclient.data.api.dto

data class TariffsPageDto(
    val items: List<TariffDto>?,
    val pageInfo: PageInfoDto
)
