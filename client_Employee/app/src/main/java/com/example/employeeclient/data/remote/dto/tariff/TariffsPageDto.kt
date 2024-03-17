package com.example.employeeclient.data.remote.dto.tariff

import com.example.employeeclient.data.remote.dto.common.PageInfoDto

data class TariffsPageDto(
    val items: List<TariffDto>?,
    val pageInfo: PageInfoDto
)