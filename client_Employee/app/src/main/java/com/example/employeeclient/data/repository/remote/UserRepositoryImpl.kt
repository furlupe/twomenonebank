package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.UserApi
import com.example.employeeclient.data.remote.dto.user.toDomain
import com.example.employeeclient.data.remote.dto.user.toDto
import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.model.user.UserDomain
import com.example.employeeclient.domain.model.user.UsersPageDomain
import com.example.employeeclient.domain.repository.remote.UserRepository

class UserRepositoryImpl(
    private val api: UserApi
) : UserRepository {
    override suspend fun register(body: RegisterInfoDomain) {
        return api.register(body.toDto())
    }

    override suspend fun banUserById(id: String) {
        return api.banUserById(id)
    }

    override suspend fun unbanUserById(id: String) {
        return api.unbanUserById(id)
    }

    override suspend fun getUsers(page: Int): UsersPageDomain {
        return api.getUsers(page).toDomain()
    }

    override suspend fun getUser(userId: String): UserDomain {
        return api.getUser(userId).toDomain()
    }
}