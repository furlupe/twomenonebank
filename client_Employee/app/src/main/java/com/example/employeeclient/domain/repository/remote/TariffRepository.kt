package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.domain.model.tariff.TariffCreateDomain
import com.example.employeeclient.domain.model.tariff.TariffsPageDomain

interface TariffRepository {
    suspend fun getAllTariffs(page: Int): TariffsPageDomain
    suspend fun createTariff(body: TariffCreateDomain)
}