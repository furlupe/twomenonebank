package com.example.employeeclient.data.repository.db

import com.example.employeeclient.data.db.dao.TariffDao
import com.example.employeeclient.domain.model.db.toDomain
import com.example.employeeclient.domain.model.db.toEntity
import com.example.employeeclient.domain.model.tariff.TariffDomain
import com.example.employeeclient.domain.repository.db.TariffSyncRepository

class TariffSyncRepositoryImpl(
    private val dao: TariffDao
): TariffSyncRepository {
    override suspend fun upsert(tariff: TariffDomain) {
        dao.upsert(tariff.toEntity())
    }

    override suspend fun getAll(): List<TariffDomain> {
        return dao.getAll().map { it.toDomain() }
    }

    override suspend fun delete(ban: TariffDomain) {
        dao.delete(ban.toEntity())
    }
}