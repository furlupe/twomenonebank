package com.example.employeeclient.di

import com.example.employeeclient.domain.usecase.auth.ConnectRefreshUseCase
import com.example.employeeclient.domain.usecase.auth.ConnectUseCase
import com.example.employeeclient.domain.usecase.db.SyncAllDataUseCase
import com.example.employeeclient.domain.usecase.db.ban.InsertBanUseCase
import com.example.employeeclient.domain.usecase.db.ban.SyncBansUseCase
import com.example.employeeclient.domain.usecase.db.tariff.InsertTariffUseCase
import com.example.employeeclient.domain.usecase.db.tariff.SyncTariffsUseCase
import com.example.employeeclient.domain.usecase.db.user.InsertUserUseCase
import com.example.employeeclient.domain.usecase.db.user.SyncUsersUseCase
import com.example.employeeclient.domain.usecase.token.DeleteTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.GetTokenIsAliveFromLocalStorageUseCase
import com.example.employeeclient.domain.usecase.token.SaveTokenToLocalStorageUseCase
import com.example.employeeclient.domain.usecase.users.BanUserUseCase
import com.example.employeeclient.domain.usecase.users.GetUserByIdUseCase
import com.example.employeeclient.domain.usecase.users.GetUsersPageUseCase
import com.example.employeeclient.domain.usecase.users.RegisterUseCase
import com.example.employeeclient.domain.usecase.users.UnbanUserUseCase
import org.koin.dsl.module

val useCaseModule = module {
    includes(appModule)

    // - Token
    factory { SaveTokenToLocalStorageUseCase(get()) }
    factory { GetTokenFromLocalStorageUseCase(get()) }
    factory { GetTokenIsAliveFromLocalStorageUseCase(get()) }
    factory { DeleteTokenFromLocalStorageUseCase(get()) }

    // - Auth
    factory { ConnectUseCase(get(), get()) }
    factory { ConnectRefreshUseCase(get(), get(), get(), get()) }

    // - Users
    factory { RegisterUseCase(get()) }
    factory { GetUserByIdUseCase(get()) }
    factory { BanUserUseCase(get()) }
    factory { UnbanUserUseCase(get()) }
    factory { GetUsersPageUseCase(get()) }

    // Sync
    factory { InsertBanUseCase(get()) }
    factory { SyncBansUseCase(get(), get()) }

    factory { InsertUserUseCase(get()) }
    factory { SyncUsersUseCase(get(), get()) }

    factory { InsertTariffUseCase(get()) }
    factory { SyncTariffsUseCase(get(), get()) }

    factory { SyncAllDataUseCase(get(), get(), get()) }

}