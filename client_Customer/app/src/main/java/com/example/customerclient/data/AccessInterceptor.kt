package com.example.customerclient.data

import com.example.customerclient.data.storage.SharedPreferencesRepositoryImpl
import com.example.customerclient.domain.repositories.AuthRepository
import kotlinx.coroutines.runBlocking
import okhttp3.Interceptor
import okhttp3.Response


class AccessInterceptor(
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl,
    private val authRepository: AuthRepository
    ) : Interceptor {

    override fun intercept(chain: Interceptor.Chain): Response {
        val originalRequest = chain.request()
        val accessToken = sharedPreferencesRepositoryImpl.getAccessToken()
        val expiredTime = sharedPreferencesRepositoryImpl.getExpiredTime()
        val currentTime = System.currentTimeMillis() / 1000

        if (accessToken != "" && currentTime < expiredTime) {
            val refreshToken = sharedPreferencesRepositoryImpl.getRefreshToken()

            val refreshedToken = runBlocking {
                val response = authRepository.refreshToken(refreshToken)
                sharedPreferencesRepositoryImpl.saveTokens(response)
                response.access_token
            }

            if (refreshedToken != "") {
                val newRequest = originalRequest.newBuilder()
                    .header("Authorization", "Bearer $refreshedToken")
                    .build()

                return chain.proceed(newRequest)
            }
        }

        val authorizedRequest = originalRequest.newBuilder()
            .header("Authorization", "Bearer $accessToken")
            .build()

        return chain.proceed(authorizedRequest)
    }
}
