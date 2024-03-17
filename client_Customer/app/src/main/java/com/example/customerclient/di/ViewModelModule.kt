package com.example.customerclient.di

import androidx.lifecycle.SavedStateHandle
import com.example.customerclient.ui.auth.signin.SignInViewModel
import com.example.customerclient.ui.bill.all.AllBillsViewModel
import com.example.customerclient.ui.bill.info.BillInfoViewModel
import com.example.customerclient.ui.bottombar.home.HomeViewModel
import com.example.customerclient.ui.credit.all.AllCreditsViewModel
import com.example.customerclient.ui.credit.create.CreateCreditViewModel
import com.example.customerclient.ui.credit.info.CreditInfoViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val viewModelModule = module {
    includes(appModule)
    includes(useCaseModule)

    viewModel { SignInViewModel(signInUseCase = get()) }

    viewModel {
        HomeViewModel(
            getUserInfoUseCase = get(),
            getUserBillsInfoUseCase = get(),
            getUserCreditsInfoUseCase = get(),
            getDollarExchangeRateUseCase = get(),
            getEuroExchangeRateUseCase = get(),
        )
    }

    viewModel { AllBillsViewModel(getUserBillsInfoUseCase = get()) }
    viewModel { (handle: SavedStateHandle) ->
        BillInfoViewModel(
            handle,
            getBillInfoUseCase = get()
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
    viewModel { AllCreditsViewModel(getUserCreditsInfoPagingUseCase = get()) }
    viewModel {
        CreateCreditViewModel(
            createCreditUseCase = get(),
            getCreditTariffsUseCase = get()
        )
    }
}