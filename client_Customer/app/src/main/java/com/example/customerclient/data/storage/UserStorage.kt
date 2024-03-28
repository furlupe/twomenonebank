package com.example.customerclient.data.storage

interface UserStorage {
    companion object {
        const val USER_ID = "userId"
        const val USER_THEME = "userTheme"
    }

    fun saveUserId(userId: String)

    fun getUserId(): String

    fun swipeUserTheme()
    fun getCurrentUserTheme(): UserTheme
}

enum class UserTheme {
    LIGHT, DARK
}