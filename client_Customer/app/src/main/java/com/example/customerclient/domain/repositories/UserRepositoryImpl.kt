package com.example.customerclient.domain.repositories

import com.example.customerclient.data.repository.UserRepository
import com.example.customerclient.userName

class UserRepositoryImpl : UserRepository {
    override fun getUserName(userId: String): String {
        return userName
    }
}