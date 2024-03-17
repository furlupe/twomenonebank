package com.example.customerclient.data.repository

import com.example.customerclient.data.api.auth.UserApi
import com.example.customerclient.data.api.dto.RegisterDto
import com.example.customerclient.data.api.dto.toUserInfo
import com.example.customerclient.data.storage.SharedPreferencesRepositoryImpl
import com.example.customerclient.domain.models.UserInfo
import com.example.customerclient.domain.repositories.UserRepository

class UserRepositoryImpl(
    private val userApi: UserApi,
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl
) : UserRepository {
    override suspend fun getUserInfo(): UserInfo {
        val user = userApi.getUserInfo()
        sharedPreferencesRepositoryImpl.saveUserId(userId = user.id)
        return user.toUserInfo()
    }

    override suspend fun registerUser(email: String, password: String, userName: String) {
        return userApi.registerUser(RegisterDto(email, password, userName))
    }

}