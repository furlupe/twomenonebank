package com.example.employeeclient.di

import androidx.room.Room
import com.example.employeeclient.common.Constants.BASE_BFF_URL
import com.example.employeeclient.common.Constants.DATABASE_NAME
import com.example.employeeclient.data.db.SyncDatabase
import com.example.employeeclient.data.remote.api.AccountApi
import com.example.employeeclient.data.remote.api.AuthApi
import com.example.employeeclient.data.remote.api.CreditApi
import com.example.employeeclient.data.remote.api.TariffApi
import com.example.employeeclient.data.remote.api.UserApi
import com.example.employeeclient.data.remote.network.OkHttpProvider
import com.example.employeeclient.data.repository.db.BanSyncRepositoryImpl
import com.example.employeeclient.data.repository.db.TariffSyncRepositoryImpl
import com.example.employeeclient.data.repository.db.UserSyncRepositoryImpl
import com.example.employeeclient.data.repository.remote.AccountRepositoryImpl
import com.example.employeeclient.data.repository.remote.AuthRepositoryImpl
import com.example.employeeclient.data.repository.remote.CreditRepositoryImpl
import com.example.employeeclient.data.repository.remote.TariffRepositoryImpl
import com.example.employeeclient.data.repository.remote.UserRepositoryImpl
import com.example.employeeclient.data.repository.storage.TokenRepositoryImpl
import com.example.employeeclient.data.storage.EncryptedSharedPreferencesStorage
import com.example.employeeclient.domain.repository.db.BanSyncRepository
import com.example.employeeclient.domain.repository.db.TariffSyncRepository
import com.example.employeeclient.domain.repository.db.UserSyncRepository
import com.example.employeeclient.domain.repository.remote.AccountRepository
import com.example.employeeclient.domain.repository.remote.AuthRepository
import com.example.employeeclient.domain.repository.remote.CreditRepository
import com.example.employeeclient.domain.repository.remote.TariffRepository
import com.example.employeeclient.domain.repository.remote.UserRepository
import com.example.employeeclient.domain.repository.storage.TokenRepository
import com.google.gson.GsonBuilder
import org.koin.android.ext.koin.androidApplication
import org.koin.android.ext.koin.androidContext
import org.koin.dsl.module
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

val appModule = module {

    /* Database */
    single {
        Room.databaseBuilder(
            androidApplication(),
            SyncDatabase::class.java,
            DATABASE_NAME
        ).build()
    }

    // Daos
    single { get<SyncDatabase>().banDao() }
    single { get<SyncDatabase>().tariffDao() }
    single { get<SyncDatabase>().userDao() }

    single<BanSyncRepository> { BanSyncRepositoryImpl(get()) }
    single<TariffSyncRepository> { TariffSyncRepositoryImpl(get()) }
    single<UserSyncRepository> { UserSyncRepositoryImpl(get()) }

    /* API */
    val gson = GsonBuilder()
        .setLenient()
        .create()

    single { EncryptedSharedPreferencesStorage(androidContext()) }
    single<TokenRepository> { TokenRepositoryImpl(get()) }

    single {
        Retrofit.Builder()
            .baseUrl(BASE_BFF_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(OkHttpProvider.provideClient(get()))
            .build()
            .create(AuthApi::class.java)
    }
    single<AuthRepository> {
        AuthRepositoryImpl(get())
    }

    single {
        Retrofit.Builder()
            .baseUrl(BASE_BFF_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(OkHttpProvider.provideClient(get(), get(), get()))
            .build()
            .create(UserApi::class.java)
    }
    single<UserRepository> {
        UserRepositoryImpl(get())
    }

    single {
        Retrofit.Builder()
            .baseUrl(BASE_BFF_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(OkHttpProvider.provideClient(get(), get(), get()))
            .build()
            .create(CreditApi::class.java)
    }
    single<CreditRepository> {
        CreditRepositoryImpl(get())
    }

    single {
        Retrofit.Builder()
            .baseUrl(BASE_BFF_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(OkHttpProvider.provideClient(get(), get(), get()))
            .build()
            .create(TariffApi::class.java)
    }
    single<TariffRepository> {
        TariffRepositoryImpl(get())
    }

    single {
        Retrofit.Builder()
            .baseUrl(BASE_BFF_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .client(OkHttpProvider.provideClient(get(), get(), get()))
            .build()
            .create(AccountApi::class.java)
    }
    single<AccountRepository> {
        AccountRepositoryImpl(get())
    }

}