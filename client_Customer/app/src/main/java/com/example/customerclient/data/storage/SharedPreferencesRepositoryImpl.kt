package com.example.customerclient.data.storage

import com.example.customerclient.data.api.dto.TokenDto

class SharedPreferencesRepositoryImpl(
    private val sharedPreferencesStorage: SharedPreferencesStorage
) {

    fun saveTokens(tokens: TokenDto) {
        return sharedPreferencesStorage.saveTokens(tokens)
    }

    fun getRefreshToken(): String {
        return sharedPreferencesStorage.getRefreshToken()
    }

    fun getAccessToken(): String {
        return sharedPreferencesStorage.getAccessToken()
    }

    fun getExpiredTime(): Long {
        return sharedPreferencesStorage.getExpiredTime()
    }

    fun deleteTokens() {
        return sharedPreferencesStorage.deleteTokens()
    }

    fun saveUserId(userId: String) {
        return sharedPreferencesStorage.saveUserId(userId)
    }

    fun getUserId(): String {
        return sharedPreferencesStorage.getUserId()
    }
}
