package com.example.employeeclient.domain.usecase.token

import com.example.employeeclient.domain.repository.storage.TokenRepository

class DeleteTokenFromLocalStorageUseCase(
    private val tokenRepository: TokenRepository
) {
    operator fun invoke() = tokenRepository.deleteTokenFromLocalStorage()
}