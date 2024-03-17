package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.credit.create.Tariff

data class TariffDto(
    val id: String,
    val name: String?,
    val rate: Int
)

fun TariffDto.toTariff() = Tariff(
    id = this.id,
    name = this.name ?: "",
    rate = this.rate.toString()
)
