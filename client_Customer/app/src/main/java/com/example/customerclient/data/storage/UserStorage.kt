package com.example.customerclient.data.storage

interface UserStorage {
    companion object {
        const val USER_ID = "userId"
    }

    fun saveUserId(userId: String)

    fun getUserId(): String
}