package com.example.customerclient.domain.repositories

interface UserRepository {
    suspend fun getUserName(userId: String): String
}