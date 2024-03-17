package com.example.customerclient.data.repository

import android.util.Log
import com.example.customerclient.data.api.auth.AuthenticationApi
import com.example.customerclient.data.api.dto.TokenDto
import com.example.customerclient.data.storage.SharedPreferencesRepositoryImpl
import com.example.customerclient.domain.repositories.AuthRepository
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext

class AuthRepositoryImpl(
    private val api: AuthenticationApi,
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl
) : AuthRepository {
    override suspend fun authorize(email: String, password: String): TokenDto {
        return withContext(Dispatchers.IO) {
            val tokens = api.connect(username = email, password = password)
            val expiresIn = tokens.expires_in
            sharedPreferencesRepositoryImpl.saveTokens(tokens.copy(expires_in = expiresIn + System.currentTimeMillis() / 1000))
            Log.d("TOKENS", "$tokens")
            tokens
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