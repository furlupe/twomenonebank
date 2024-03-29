package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.data.remote.dto.TokenDto

interface AuthRepository {
    suspend fun authorize(deeplink: String)
    suspend fun authorize()
    suspend fun connect(code: String): TokenDto
    suspend fun connectRefresh(refreshToken: String): TokenDto
}