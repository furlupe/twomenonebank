package com.example.employeeclient.data.repository.db

import com.example.employeeclient.data.db.dao.UserDao
import com.example.employeeclient.domain.model.db.toDomain
import com.example.employeeclient.domain.model.db.toEntity
import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.repository.db.UserSyncRepository

class UserSyncRepositoryImpl(
    private val dao: UserDao
): UserSyncRepository {
    override suspend fun upsert(user: RegisterInfoDomain) {
        dao.upsert(user.toEntity())
    }

    override suspend fun getAll(): List<RegisterInfoDomain> {
        return dao.getAll().map { it.toDomain() }
    }

    override suspend fun delete(user: RegisterInfoDomain) {
        dao.delete(user.toEntity())
    }
}