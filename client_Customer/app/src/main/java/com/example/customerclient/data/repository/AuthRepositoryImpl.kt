package com.example.customerclient.data.repository

import com.example.customerclient.data.api.auth.AuthenticationApi
import com.example.customerclient.data.api.dto.TokenDto
import com.example.customerclient.domain.repositories.AuthRepository
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext

class AuthRepositoryImpl(
    private val api: AuthenticationApi,
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl
) : AuthRepository {
    override suspend fun authorize() {
        return withContext(Dispatchers.IO) {
            api.authorize()
        }
    }

    override suspend fun signIn(code: String) {
        return withContext(Dispatchers.IO) {
            val tokens = api.connect(code = code)
            val expiresIn = tokens.expires_in
            sharedPreferencesRepositoryImpl.saveTokens(tokens.copy(expires_in = expiresIn + System.currentTimeMillis() / 1000))
        }
    }

    override suspend fun refreshToken(refreshToken: String): TokenDto {
        return withContext(Dispatchers.IO) {
            val tokens = api.connect(refreshToken = refreshToken)
            sharedPreferencesRepositoryImpl.saveTokens(tokens)
            tokens
        }
    }
}