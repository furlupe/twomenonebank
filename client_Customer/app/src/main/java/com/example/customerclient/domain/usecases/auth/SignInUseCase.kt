package com.example.customerclient.domain.usecases.auth

import com.example.customerclient.domain.repositories.AuthRepository

class SignInUseCase(private val authRepository: AuthRepository) {
    suspend operator fun invoke(username: String, password: String) =
        authRepository.authorize(username, password)
}