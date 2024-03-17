package com.example.employeeclient.data.remote.network

import com.example.employeeclient.common.Constants.CONNECT_TIMEOUT
import com.example.employeeclient.common.Constants.READ_TIMEOUT
import com.example.employeeclient.common.Constants.WRITE_TIMEOUT
import com.example.employeeclient.domain.usecase.auth.ConnectRefreshUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenIsAliveFromLocalStorageUseCase
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import java.util.concurrent.TimeUnit

class OkHttpProvider {
    companion object {
        fun provideClient(
            getTokenFromLocalStorageUseCase: GetTokenFromLocalStorageUseCase,
            getTokenIsAliveFromLocalStorageUseCase: GetTokenIsAliveFromLocalStorageUseCase,
            connectRefreshUseCase: ConnectRefreshUseCase
        ): OkHttpClient {
            val client = OkHttpClient.Builder().apply {
                connectTimeout(CONNECT_TIMEOUT, TimeUnit.SECONDS)
                readTimeout(READ_TIMEOUT, TimeUnit.SECONDS)
                writeTimeout(WRITE_TIMEOUT, TimeUnit.SECONDS)
                addInterceptor(
                    Interceptor(
                        getTokenFromLocalStorageUseCase,
                        getTokenIsAliveFromLocalStorageUseCase,
                        connectRefreshUseCase
                    )
                )

                val logLevel = HttpLoggingInterceptor.Level.BODY
                addInterceptor(HttpLoggingInterceptor().setLevel(logLevel))
            }

            return client.build()
        }

        fun provideClient(
            getTokenFromLocalStorageUseCase: GetTokenFromLocalStorageUseCase,
        ): OkHttpClient {
            val client = OkHttpClient.Builder().apply {
                connectTimeout(CONNECT_TIMEOUT, TimeUnit.SECONDS)
                readTimeout(READ_TIMEOUT, TimeUnit.SECONDS)
                writeTimeout(WRITE_TIMEOUT, TimeUnit.SECONDS)
                addInterceptor(
                    Interceptor(getTokenFromLocalStorageUseCase)
                )

                val logLevel = HttpLoggingInterceptor.Level.BODY
                addInterceptor(HttpLoggingInterceptor().setLevel(logLevel))
            }

            return client.build()
        }
    }
}