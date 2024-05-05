package com.example.customerclient.domain.usecases.notification

import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl

class SaveUserTokenUseCase(private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl) {
    suspend operator fun invoke(userToken: String) =
        sharedPreferencesRepositoryImpl.saveUserToken(userToken)
}