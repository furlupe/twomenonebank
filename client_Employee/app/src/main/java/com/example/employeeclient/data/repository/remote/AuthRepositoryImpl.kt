package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.AuthApi
import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository

class AuthRepositoryImpl(
    private val api: AuthApi
): AuthRepository {

    override suspend fun authorize() {
        api.authorize()
    }

    override suspend fun connect(code: String): TokenDto {
        return api.connect(code = code)
    }

    override suspend fun connectRefresh(refreshToken: String): TokenDto {
        return api.connectRefresh(refreshToken = refreshToken)
    }
}