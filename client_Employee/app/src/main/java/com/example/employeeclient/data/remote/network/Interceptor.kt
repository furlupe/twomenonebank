package com.example.employeeclient.data.remote.network

import com.example.employeeclient.domain.usecase.auth.ConnectRefreshUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenIsAliveFromLocalStorageUseCase
import kotlinx.coroutines.runBlocking
import okhttp3.Interceptor
import okhttp3.Request
import okhttp3.Response
import java.io.IOException
import java.util.UUID

class Interceptor(
    private val getTokenFromLocalStorageUseCase: GetTokenFromLocalStorageUseCase,
    private val getTokenIsAliveFromLocalStorageUseCase: GetTokenIsAliveFromLocalStorageUseCase? = null,
    private val connectRefreshUseCase: ConnectRefreshUseCase? = null
) : Interceptor {

    @Throws(IOException::class)
    override fun intercept(chain: Interceptor.Chain): Response {
        var token = getTokenFromLocalStorageUseCase()

        val getTokenAlivenessUseCase = getTokenIsAliveFromLocalStorageUseCase
        val refreshTokeUseCase = connectRefreshUseCase
        if (getTokenAlivenessUseCase != null && refreshTokeUseCase != null) {
            val isTokenAlive = getTokenAlivenessUseCase()
            if (!isTokenAlive && token.access_token.isNotBlank()) {
                token = runBlocking { refreshTokeUseCase() }
            }
        }

        val request: Request = chain.request()
        val newRequest: Request = if (token.access_token.isNotBlank()) {
            newRequestWithAccessToken(request, token.access_token)
        } else {
            request.newBuilder()
                .addHeader("Accept", "application/json")
                .addHeader("Content-Type", "application/json")
                .build()
        }

        val response = chain.proceed(newRequest)
        if (response.code == 400) {
            throw IOException(response.message)
        }
        if (response.code == 404) {
            throw IOException("HTTP 404")
        }
        if (response.code == 302) {
            val location = response.header("Location")
            if (location != null) {
                throw RedirectException(location)
            }
        }
        return response
    }

    private fun newRequestWithAccessToken(
        request: Request,
        accessToken: String
    ): Request {
        return request.newBuilder()
            .addHeader("Accept", "application/json")
            .addHeader("Content-Type", "application/json")
            .addHeader("Authorization", "Bearer $accessToken")
            .addHeader("Idempotence-Key", UUID.randomUUID().toString())
            .build()
    }
}

class RedirectException(val redirectUrl: String) : IOException()
