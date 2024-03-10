package com.example.customerclient.domain.usecases.user

import com.example.customerclient.data.repository.UserRepository

class GetUserNameUseCase(
    private val userRepository: UserRepository
) {
    suspend operator fun invoke(userId: String): String {
        return userRepository.getUserName(userId)
    }
}