package com.example.customerclient.domain.usecases.auth

import com.example.customerclient.domain.repositories.AuthRepository

class AuthorizeUseCase(private val authRepository: AuthRepository) {
    suspend operator fun invoke() =
        authRepository.authorize()
}