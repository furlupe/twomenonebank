package com.example.employeeclient.domain.usecase.users

import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.repository.remote.UserRepository

class RegisterUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(body: RegisterInfoDomain) {
        repository.register(body)
    }
}