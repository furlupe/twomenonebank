package com.example.customerclient.domain.usecases.auth

import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl

class GetTokenUseCase(private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl) {
    operator fun invoke(): String = sharedPreferencesRepositoryImpl.getAccessToken()
}