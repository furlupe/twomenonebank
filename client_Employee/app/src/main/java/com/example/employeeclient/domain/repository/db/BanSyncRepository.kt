package com.example.employeeclient.domain.repository.db

import com.example.employeeclient.domain.model.db.BanDomain

interface BanSyncRepository {
    suspend fun upsert(ban: BanDomain)
    suspend fun getAll(): List<BanDomain>
    suspend fun delete(ban: BanDomain)
}