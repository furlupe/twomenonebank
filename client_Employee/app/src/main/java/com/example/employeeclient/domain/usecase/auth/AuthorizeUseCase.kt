package com.example.employeeclient.domain.usecase.auth

import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository
import com.example.employeeclient.domain.usecase.token.SaveTokenToLocalStorageUseCase

class AuthorizeUseCase(
    private val repository: AuthRepository,
) {
    suspend operator fun invoke() {
        repository.authorize()
    }
}