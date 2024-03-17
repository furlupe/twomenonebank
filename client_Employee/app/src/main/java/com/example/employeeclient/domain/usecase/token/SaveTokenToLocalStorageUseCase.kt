package com.example.employeeclient.domain.usecase.token

import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.storage.TokenRepository

class SaveTokenToLocalStorageUseCase(
    private val tokenRepository: TokenRepository
) {
    operator fun invoke(token: TokenDto) = tokenRepository.saveTokenToLocalStorage(token)
}