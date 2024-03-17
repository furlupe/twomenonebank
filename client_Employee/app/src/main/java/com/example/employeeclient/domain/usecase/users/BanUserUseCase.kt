package com.example.employeeclient.domain.usecase.users

import com.example.employeeclient.domain.repository.remote.UserRepository

class BanUserUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(id: String) {
        repository.banUserById(id)
    }
}