package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.AuthApi
import com.example.employeeclient.data.remote.dto.ConnectBodyDto
import com.example.employeeclient.data.remote.dto.IsDarkThemeDto
import com.example.employeeclient.data.remote.dto.RefreshTokenDto
import com.example.employeeclient.data.remote.dto.TokenDto
import com.example.employeeclient.domain.repository.remote.AuthRepository

class AuthRepositoryImpl(
    private val api: AuthApi
): AuthRepository {

    override suspend fun authorize() {
        api.authorize()
    }

    override suspend fun connect(code: String): TokenDto {
        return api.connect(ConnectBodyDto(code = code))
    }

    override suspend fun connectRefresh(refreshToken: String): TokenDto {
        return api.connectRefresh(RefreshTokenDto(refreshToken = refreshToken))
    }

    override suspend fun updateUserTheme(isDark: Boolean) {
        api.updateUserTheme(isDark)
    }

    override suspend fun getIsDarkTheme(): IsDarkThemeDto {
        return api.getIsDarkTheme()
    }
}