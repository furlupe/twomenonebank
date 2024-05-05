package com.example.customerclient.domain.repositories

interface UserTokenRepository {
    suspend fun subscribeToNotification()
}