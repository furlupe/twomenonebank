package com.example.customerclient.data.storage

import com.example.customerclient.data.api.dto.TokenDto

interface TokenStorage {
    companion object {
        const val ACCESS_TOKEN = "accessToken"
        const val REFRESH_TOKEN = "refreshToken"
        const val EXPIRED_IN = "expiredIn"
    }

    fun saveTokens(token: TokenDto)

    fun getRefreshToken(): String

    fun getAccessToken(): String

    fun getExpiredTime(): Long

    fun deleteTokens()
}
