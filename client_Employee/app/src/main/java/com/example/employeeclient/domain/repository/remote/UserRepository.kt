package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.model.user.UserDomain
import com.example.employeeclient.domain.model.user.UsersPageDomain

interface UserRepository {
    suspend fun register(body: RegisterInfoDomain)
    suspend fun banUserById(id: String)
    suspend fun unbanUserById(id: String)
    suspend fun getUsers(page: Int): UsersPageDomain
    suspend fun getUser(userId: String): UserDomain
}