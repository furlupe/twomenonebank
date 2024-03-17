package com.example.employeeclient.data.repository.db

import com.example.employeeclient.data.db.dao.BanDao
import com.example.employeeclient.domain.model.db.BanDomain
import com.example.employeeclient.domain.model.db.toDomain
import com.example.employeeclient.domain.model.db.toEntity
import com.example.employeeclient.domain.repository.db.BanSyncRepository

class BanSyncRepositoryImpl(
    private val dao: BanDao
): BanSyncRepository {
    override suspend fun upsert(ban: BanDomain) {
        dao.upsert(ban.toEntity())
    }

    override suspend fun getAll(): List<BanDomain> {
        return dao.getAll().map { it.toDomain() }
    }

    override suspend fun delete(ban: BanDomain) {
        dao.delete(ban.toEntity())
    }
}