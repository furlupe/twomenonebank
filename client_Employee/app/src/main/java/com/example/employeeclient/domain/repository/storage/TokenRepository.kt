package com.example.employeeclient.domain.repository.storage

import com.example.employeeclient.data.remote.dto.TokenDto

interface TokenRepository {
    fun saveTokenToLocalStorage(token: TokenDto)

    fun getTokenFromLocalStorage(): TokenDto

    fun deleteTokenFromLocalStorage()

    fun isTokenAlive(): Boolean
}