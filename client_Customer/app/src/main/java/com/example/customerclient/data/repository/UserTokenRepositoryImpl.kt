package com.example.customerclient.data.repository

import com.example.customerclient.data.api.dto.UserTokenDto
import com.example.customerclient.data.api.notification.UserTokenApi
import com.example.customerclient.domain.repositories.UserTokenRepository

class UserTokenRepositoryImpl(
    private val userTokenApi: UserTokenApi,
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl
) : UserTokenRepository {
    override suspend fun subscribeToNotification() {
        val token = sharedPreferencesRepositoryImpl.getUserToken()
        userTokenApi.createNotification(UserTokenDto(token))
    }
}