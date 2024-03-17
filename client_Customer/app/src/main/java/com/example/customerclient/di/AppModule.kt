package com.example.customerclient.di

import com.example.customerclient.common.Constants
import com.example.customerclient.data.AccessInterceptor
import com.example.customerclient.data.api.auth.AuthenticationApi
import com.example.customerclient.data.api.auth.UserApi
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.repository.AuthRepositoryImpl
import com.example.customerclient.data.repository.BillRepositoryImpl
import com.example.customerclient.data.repository.CreditRepositoryImpl
import com.example.customerclient.data.repository.ExchangeRateRepositoryImpl
import com.example.customerclient.data.repository.UserRepositoryImpl
import com.example.customerclient.data.storage.SharedPreferencesRepositoryImpl
import com.example.customerclient.data.storage.SharedPreferencesStorage
import com.example.customerclient.domain.repositories.AuthRepository
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.domain.repositories.ExchangeRateRepository
import com.example.customerclient.domain.repositories.UserRepository
import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import org.koin.android.ext.koin.androidContext
import org.koin.dsl.module
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

val appModule = module {
    val gson = GsonBuilder()
        .setLenient()
        .create()


    single<SharedPreferencesStorage> { SharedPreferencesStorage(androidContext()) }
    single<SharedPreferencesRepositoryImpl> {
        SharedPreferencesRepositoryImpl(
            sharedPreferencesStorage = get()
        )
    }

    single<Retrofit> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_AUTH_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build()
    }

    // - Auth
    single<AuthenticationApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_AUTH_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(AuthenticationApi::class.java)
    }
    single<AuthRepository> {
        AuthRepositoryImpl(
            api = get(),
            sharedPreferencesRepositoryImpl = get()
        )
    }

    // - User
    single<UserApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_AUTH_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(UserApi::class.java)
    }
    single<UserRepository> {
        UserRepositoryImpl(
            userApi = get(),
            sharedPreferencesRepositoryImpl = get()
        )
    }

    // - Credits
    single<CreditsApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_CREDIT_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(CreditsApi::class.java)
    }
    single<CreditRepository> {
        CreditRepositoryImpl(creditsApi = get())
    }

    // region : Repositories
    single<BillRepository> { BillRepositoryImpl() }
    single<ExchangeRateRepository> { ExchangeRateRepositoryImpl() }
    // end region
}
