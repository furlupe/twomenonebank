package com.example.employeeclient.domain.usecase.users

import com.example.employeeclient.domain.repository.remote.UserRepository

class GetUsersPageUseCase(
    private val repository: UserRepository
) {
    suspend operator fun invoke(page: Int) = repository.getUsers(page)
}