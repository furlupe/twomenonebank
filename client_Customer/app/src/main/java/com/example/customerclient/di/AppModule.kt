package com.example.customerclient.di

import androidx.room.Room
import com.example.customerclient.common.Constants
import com.example.customerclient.common.Constants.BILL_DATABASE_NAME
import com.example.customerclient.common.Constants.CREDIT_DATABASE_NAME
import com.example.customerclient.data.AccessInterceptor
import com.example.customerclient.data.api.auth.AuthenticationApi
import com.example.customerclient.data.api.auth.UserApi
import com.example.customerclient.data.api.core.AccountsApi
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.api.notification.UserTokenApi
import com.example.customerclient.data.api.transactions.TransactionsApi
import com.example.customerclient.data.remote.database.BillDatabase
import com.example.customerclient.data.remote.database.CreditDatabase
import com.example.customerclient.data.repository.AuthRepositoryImpl
import com.example.customerclient.data.repository.BillHistoryWebSocketRepositoryImpl
import com.example.customerclient.data.repository.BillRepositoryImpl
import com.example.customerclient.data.repository.CreditRepositoryImpl
import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl
import com.example.customerclient.data.repository.TransactionRepositoryImpl
import com.example.customerclient.data.repository.UserRepositoryImpl
import com.example.customerclient.data.repository.UserTokenRepositoryImpl
import com.example.customerclient.data.storage.SharedPreferencesStorage
import com.example.customerclient.data.websocket.BillHistoryWebSocket
import com.example.customerclient.domain.repositories.AuthRepository
import com.example.customerclient.domain.repositories.BillHistoryWebSocketRepository
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.domain.repositories.TransactionRepository
import com.example.customerclient.domain.repositories.UserRepository
import com.example.customerclient.domain.repositories.UserTokenRepository
import com.google.gson.GsonBuilder
import okhttp3.OkHttpClient
import okhttp3.logging.HttpLoggingInterceptor
import org.koin.android.ext.koin.androidApplication
import org.koin.android.ext.koin.androidContext
import org.koin.core.module.dsl.singleOf
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

    single<BillDatabase> {
        Room.databaseBuilder(
            androidApplication(),
            BillDatabase::class.java,
            BILL_DATABASE_NAME
        ).build()
    }

    singleOf(BillDatabase::billDao)

    single<CreditDatabase> {
        Room.databaseBuilder(
            androidApplication(),
            CreditDatabase::class.java,
            CREDIT_DATABASE_NAME
        ).build()
    }

    singleOf(CreditDatabase::creditDao)

    // - Auth
    single<AuthenticationApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_AUTH_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(30, TimeUnit.SECONDS)
                    .connectTimeout(30, TimeUnit.SECONDS)
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
                    .readTimeout(30, TimeUnit.SECONDS)
                    .connectTimeout(30, TimeUnit.SECONDS)
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
                    .readTimeout(30, TimeUnit.SECONDS)
                    .connectTimeout(30, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(CreditsApi::class.java)
    }
    single<CreditRepository> {
        CreditRepositoryImpl(creditsApi = get(), creditDao = get())
    }

    // - Bills
    single<AccountsApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_CORE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(15, TimeUnit.SECONDS)
                    .connectTimeout(15, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(AccountsApi::class.java)
    }

    single<BillRepository> {
        BillRepositoryImpl(accountsApi = get(), billDao = get())
    }

    single<BillHistoryWebSocket> { BillHistoryWebSocket() }

    // - Transactions
    single<TransactionsApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_TRANSACTIONS_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(TransactionsApi::class.java)
    }

    single<TransactionRepository> { TransactionRepositoryImpl(transactionsApi = get()) }

    // - WebSocket
    single<BillHistoryWebSocketRepository> { BillHistoryWebSocketRepositoryImpl(billHistoryWebSocket = get()) }

    // - Notification
    single<UserTokenApi> {
        Retrofit.Builder()
            .baseUrl(Constants.BASE_NOTIFICATION_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(
                OkHttpClient.Builder()
                    .readTimeout(60, TimeUnit.SECONDS)
                    .connectTimeout(60, TimeUnit.SECONDS)
                    .addInterceptor(AccessInterceptor(get(), get()))
                    .addInterceptor(HttpLoggingInterceptor().setLevel(HttpLoggingInterceptor.Level.BODY))
                    .build()
            )
            .build().create(UserTokenApi::class.java)
    }

    single<UserTokenRepository> {
        UserTokenRepositoryImpl(
            userTokenApi = get(),
            sharedPreferencesRepositoryImpl = get()
        )
    }

}
