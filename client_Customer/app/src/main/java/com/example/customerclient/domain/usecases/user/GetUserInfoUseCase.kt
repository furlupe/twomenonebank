package com.example.customerclient.domain.usecases.user

import com.example.customerclient.domain.models.UserInfo
import com.example.customerclient.domain.repositories.UserRepository

class GetUserInfoUseCase(
    private val userRepository: UserRepository
) {
    suspend operator fun invoke(): UserInfo = userRepository.getUserInfo()
}