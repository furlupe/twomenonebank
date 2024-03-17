package com.example.employeeclient.domain.usecase.users

import com.example.employeeclient.domain.repository.remote.UserRepository

class GetUserByIdUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(userId: String) = repository.getUser(userId)
}