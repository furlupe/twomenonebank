package com.example.employeeclient.domain.repository.db

import com.example.employeeclient.domain.model.user.RegisterInfoDomain

interface UserSyncRepository {
    suspend fun upsert(user: RegisterInfoDomain)
    suspend fun getAll(): List<RegisterInfoDomain>
    suspend fun delete(user: RegisterInfoDomain)
}