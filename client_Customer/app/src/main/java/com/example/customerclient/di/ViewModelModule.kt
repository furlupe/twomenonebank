package com.example.customerclient.di

import androidx.lifecycle.SavedStateHandle
import com.example.customerclient.ui.MainViewModel
import com.example.customerclient.ui.auth.SignInViewModel
import com.example.customerclient.ui.bill.all.AllBillsViewModel
import com.example.customerclient.ui.bill.info.BillInfoViewModel
import com.example.customerclient.ui.credit.all.AllCreditsViewModel
import com.example.customerclient.ui.credit.create.CreateCreditViewModel
import com.example.customerclient.ui.credit.info.CreditInfoViewModel
import com.example.customerclient.ui.home.HomeViewModel
import com.example.customerclient.ui.transaction.Me2MeTransactionViewModel
import com.example.customerclient.ui.transaction.P2PTransactionViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val viewModelModule = module {
    includes(appModule)
    includes(useCaseModule)

    viewModel { MainViewModel(signInUseCase = get()) }

    viewModel { SignInViewModel(authorizeUseCase = get()) }

    viewModel {
        HomeViewModel(
            getUserInfoUseCase = get(),
            getUserBillsInfoUseCase = get(),
            getUserCreditsInfoUseCase = get(),
            openBillUseCase = get(),
            saveUserBillInfoToDatabaseUseCase = get(),
            getUserBillsInfoFromDatabaseUseCase = get(),
            getUserCreditsInfoFromDatabaseUseCase = get(),
            saveUserCreditInfoToDatabaseUseCase = get(),
            sharedPreferencesRepositoryImpl = get()
        )
    }

    viewModel {
        AllBillsViewModel(
            getUserBillsPagedInfoUseCase = get(),
            getUserBillsInfoFromDatabaseUseCase = get()
        )
    }
    viewModel { (handle: SavedStateHandle) ->
        BillInfoViewModel(
            handle,
            getBillInfoUseCase = get(),
            getBillHistoryUseCase = get(),
            depositUseCase = get(),
            withdrawUseCase = get(),
            closeBillUseCase = get()
        )
    }

    viewModel { (handle: SavedStateHandle) ->
        CreditInfoViewModel(
            handle,
            getCreditInfoUseCase = get(),
            getCreditHistoryUseCase = get(),
            payCreditUseCase = get()
        )
    }
    viewModel {
        AllCreditsViewModel(
            getUserCreditsInfoPagingUseCase = get(),
            getUserCreditsInfoFromDatabaseUseCase = get()
        )
    }
    viewModel {
        CreateCreditViewModel(
            createCreditUseCase = get(),
            getCreditTariffsUseCase = get()
        )
    }

    viewModel { (handle: SavedStateHandle) ->
        P2PTransactionViewModel(
            handle,
            p2pTransactionUseCase = get(),
        )
    }

    viewModel { (handle: SavedStateHandle) ->
        Me2MeTransactionViewModel(
            handle,
            me2MeTransactionUseCase = get(),
            getUserBillsPagedInfoUseCase = get()
        )
    }
}
