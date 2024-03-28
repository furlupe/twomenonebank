package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.data.remote.dto.TokenDto

interface AuthRepository {
    suspend fun authorize(deeplink: String)
    suspend fun connect(username: String, password: String): TokenDto
    suspend fun connect(refreshToken: String): TokenDto
}