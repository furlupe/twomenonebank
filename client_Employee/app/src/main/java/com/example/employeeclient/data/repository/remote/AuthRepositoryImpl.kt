package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.AuthApi
import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository

class AuthRepositoryImpl(
    private val api: AuthApi
): AuthRepository {
    override suspend fun connect(username: String, password: String): TokenDto {
        return api.connect(username = username, password = password)
    }

    override suspend fun connect(refreshToken: String): TokenDto {
        return api.connect(refreshToken = refreshToken)
    }
}