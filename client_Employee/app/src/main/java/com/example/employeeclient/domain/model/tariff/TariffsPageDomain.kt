package com.example.employeeclient.domain.model.tariff

data class TariffsPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val items: List<TariffDomain>
)