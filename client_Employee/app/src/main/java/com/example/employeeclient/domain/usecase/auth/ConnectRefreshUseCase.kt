package com.example.employeeclient.domain.usecase.auth

import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository
import com.example.employeeclient.domain.usecase.token.DeleteTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.SaveTokenToLocalStorageUseCase

class ConnectRefreshUseCase(
    private val repository: AuthRepository,
    private val deleteTokenFromLocalStorageUseCase: DeleteTokenFromLocalStorageUseCase,
    private val getTokenFromLocalStorageUseCase: GetTokenFromLocalStorageUseCase,
    private val saveTokenUseCase: SaveTokenToLocalStorageUseCase
) {
    suspend operator fun invoke(): TokenDto {
        val refreshToken = getTokenFromLocalStorageUseCase().refresh_token
        deleteTokenFromLocalStorageUseCase()
        val token = repository.connect(refreshToken)
        saveTokenUseCase(token)

        return token
    }
}