package com.example.customerclient.data.repository

interface UserRepository {
    fun getUserName(userId: String): String
}