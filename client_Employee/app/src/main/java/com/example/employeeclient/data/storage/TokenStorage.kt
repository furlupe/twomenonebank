package com.example.employeeclient.data.storage

import com.example.employeeclient.data.remote.dto.TokenDto

interface TokenStorage {
    companion object {
        const val TOKEN_KEY = "userToken"
        const val REFRESH_TOKEN_KEY = "refreshUserToken"
        const val TOKEN_EXPIRE_TIME = "tokenExpireTime"
        const val TOKEN_CREATION_TIME = "tokenCreationTime"
        const val TOKEN_TYPE = "tokenType"
    }

    fun saveToken(token: TokenDto)

    fun getToken(): TokenDto

    fun deleteToken()

    fun isTokenAlive(): Boolean
}