package com.example.customerclient.domain.repositories

import com.example.customerclient.domain.models.UserInfo

interface UserRepository {
    suspend fun getUserInfo(): UserInfo

    suspend fun registerUser(email: String, password: String, userName: String)
}