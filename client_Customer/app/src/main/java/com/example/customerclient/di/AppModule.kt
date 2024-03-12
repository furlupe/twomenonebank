package com.example.customerclient.di

import com.example.customerclient.data.repository.BillRepositoryImpl
import com.example.customerclient.data.repository.CreditRepositoryImpl
import com.example.customerclient.data.repository.ExchangeRateRepositoryImpl
import com.example.customerclient.data.repository.UserRepositoryImpl
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.domain.repositories.ExchangeRateRepository
import com.example.customerclient.domain.repositories.UserRepository
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.usecases.user.GetUserNameUseCase
import com.example.customerclient.ui.auth.signin.SignInViewModel
import com.example.customerclient.ui.bottombar.home.HomeViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val appModule = module {
    // region : Repositories
    single<BillRepository> { BillRepositoryImpl() }
    single<CreditRepository> { CreditRepositoryImpl() }
    single<ExchangeRateRepository> { ExchangeRateRepositoryImpl() }
    single<UserRepository> { UserRepositoryImpl() }
    // end region

    // region : Use Cases
    // - Bill
    single<GetUserBillsInfoUseCase> { GetUserBillsInfoUseCase(billRepository = get()) }

    // - Credit
    single<GetUserCreditsInfoUseCase> { GetUserCreditsInfoUseCase(creditRepository = get()) }

    // - ExchangeRate
    single<GetDollarExchangeRateUseCase> { GetDollarExchangeRateUseCase(exchangeRateRepository = get()) }
    single<GetEuroExchangeRateUseCase> { GetEuroExchangeRateUseCase(exchangeRateRepository = get()) }

    // - User
    single<GetUserNameUseCase> { GetUserNameUseCase(userRepository = get()) }
    //end region

    // region : ViewModels
    viewModel {
        HomeViewModel(
            getUserNameUseCase = get(),
            getUserBillsInfoUseCase = get(),
            getUserCreditsInfoUseCase = get(),
            getDollarExchangeRateUseCase = get(),
            getEuroExchangeRateUseCase = get()
        )
    }

    viewModel { SignInViewModel() }
    //end region
}