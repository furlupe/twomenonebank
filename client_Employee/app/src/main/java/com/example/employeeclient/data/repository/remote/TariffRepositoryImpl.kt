package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.TariffApi
import com.example.employeeclient.data.remote.dto.tariff.toDomain
import com.example.employeeclient.data.remote.dto.tariff.toDto
import com.example.employeeclient.domain.model.tariff.TariffCreateDomain
import com.example.employeeclient.domain.model.tariff.TariffsPageDomain
import com.example.employeeclient.domain.repository.remote.TariffRepository

class TariffRepositoryImpl(
    private val api: TariffApi
): TariffRepository {
    override suspend fun getAllTariffs(page: Int): TariffsPageDomain {
        return api.getAllTariffs(page).toDomain()
    }

    override suspend fun createTariff(body: TariffCreateDomain) {
        api.createTariff(body.toDto())
    }
}