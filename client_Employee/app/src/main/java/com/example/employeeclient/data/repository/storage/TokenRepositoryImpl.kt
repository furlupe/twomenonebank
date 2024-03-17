package com.example.employeeclient.data.repository.storage

import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.data.storage.EncryptedSharedPreferencesStorage
import com.example.employeeclient.domain.repository.storage.TokenRepository

class TokenRepositoryImpl(
    private val encryptedSharedPreferencesStorage: EncryptedSharedPreferencesStorage
): TokenRepository {
    override fun saveTokenToLocalStorage(token: TokenDto) {
        encryptedSharedPreferencesStorage.saveToken(token)
    }

    override fun getTokenFromLocalStorage(): TokenDto {
        return encryptedSharedPreferencesStorage.getToken()
    }

    override fun deleteTokenFromLocalStorage() {
        encryptedSharedPreferencesStorage.deleteToken()
    }

    override fun isTokenAlive(): Boolean {
        return encryptedSharedPreferencesStorage.isTokenAlive()
    }
}