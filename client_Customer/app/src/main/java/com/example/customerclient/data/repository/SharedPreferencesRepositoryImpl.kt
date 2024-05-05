package com.example.customerclient.data.repository

import com.example.customerclient.data.api.dto.TokenDto
import com.example.customerclient.data.storage.SharedPreferencesStorage
import com.example.customerclient.data.storage.UserTheme

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

    fun swipeUserTheme() = sharedPreferencesStorage.swipeUserTheme()

    fun getUserTheme(): UserTheme = sharedPreferencesStorage.getCurrentUserTheme()

    fun getUserToken(): String = sharedPreferencesStorage.getUserToken()

    fun saveUserToken(userToken: String) = sharedPreferencesStorage.saveUserToken(userToken)
}
