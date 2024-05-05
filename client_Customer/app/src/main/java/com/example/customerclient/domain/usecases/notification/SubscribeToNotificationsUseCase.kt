package com.example.customerclient.domain.usecases.notification

import com.example.customerclient.domain.repositories.UserTokenRepository

class SubscribeToNotificationsUseCase(private val userTokenRepository: UserTokenRepository) {
    suspend operator fun invoke() = userTokenRepository.subscribeToNotification()
}