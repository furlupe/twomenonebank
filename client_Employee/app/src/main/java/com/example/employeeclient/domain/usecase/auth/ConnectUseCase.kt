package com.example.employeeclient.domain.usecase.auth

import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository
import com.example.employeeclient.domain.usecase.token.SaveTokenToLocalStorageUseCase

class ConnectUseCase(
    private val repository: AuthRepository,
    private val saveTokenUseCase: SaveTokenToLocalStorageUseCase
) {
    suspend operator fun invoke(code: String): TokenDto {
        val token = repository.connect(code)
        saveTokenUseCase(token)

        return token
    }
}