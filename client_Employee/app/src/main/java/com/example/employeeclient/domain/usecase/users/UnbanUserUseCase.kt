package com.example.employeeclient.domain.usecase.users

import com.example.employeeclient.domain.repository.remote.UserRepository

class UnbanUserUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(id: String) {
        repository.unbanUserById(id)
    }
}