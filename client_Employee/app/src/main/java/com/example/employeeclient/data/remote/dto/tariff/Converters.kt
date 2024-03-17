package com.example.employeeclient.data.remote.dto.tariff

import com.example.employeeclient.domain.model.tariff.TariffCreateDomain
import com.example.employeeclient.domain.model.tariff.TariffDomain
import com.example.employeeclient.domain.model.tariff.TariffsPageDomain

fun TariffDto.toDomain() = TariffDomain(
    id = this.id,
    name = this.name ?: "Unnamed",
    rate = this.rate
)

fun TariffsPageDto.toDomain() = TariffsPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    items = this.items?.map { it.toDomain() } ?: emptyList()
)

fun TariffCreateDomain.toDto() = TariffCreateDto(
    name = this.name,
    rate = this.rate
)
