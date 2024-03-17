package com.example.customerclient.domain.repositories

import com.example.customerclient.data.api.dto.TokenDto

interface AuthRepository {
    suspend fun authorize(email: String, password: String): TokenDto
    suspend fun refreshToken(refreshToken: String): TokenDto
}