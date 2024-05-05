package com.example.customerclient.data.storage

interface UserStorage {
    companion object {
        const val USER_ID = "userId"
        const val USER_THEME = "userTheme"
        const val USER_TOKEN = "userToken"
    }

    fun saveUserId(userId: String)

    fun getUserId(): String

    fun swipeUserTheme()
    fun getCurrentUserTheme(): UserTheme

    fun saveUserToken(userToken: String)

    fun getUserToken(): String
}

enum class UserTheme {
    LIGHT, DARK
}