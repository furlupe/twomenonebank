package com.example.employeeclient.domain.repository.db

import com.example.employeeclient.domain.model.tariff.TariffDomain

interface TariffSyncRepository {
    suspend fun upsert(tariff: TariffDomain)
    suspend fun getAll(): List<TariffDomain>
    suspend fun delete(ban: TariffDomain)
}