package com.example.customerclient.data.repository

import com.example.customerclient.domain.repositories.UserRepository
import com.example.customerclient.userName

class UserRepositoryImpl : UserRepository {
    override suspend fun getUserName(userId: String): String {
        return userName
    }

}