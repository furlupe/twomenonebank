package com.example.customerclient.domain.repositories

import com.example.customerclient.data.api.dto.TokenDto

interface AuthRepository {
    suspend fun authorize()
    suspend fun signIn(code: String)
    suspend fun refreshToken(refreshToken: String): TokenDto
}