package com.example.customerclient.domain.usecases.user

import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl
import com.example.customerclient.data.storage.UserTheme

class GetUserThemeUseCase(private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl) {
    operator fun invoke(): UserTheme = sharedPreferencesRepositoryImpl.getUserTheme()
}