package com.example.customerclient.domain.repositories

interface UserRepository {
    fun getUserName(userId: String): String
}