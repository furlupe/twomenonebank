package com.example.customerclient.di

import androidx.lifecycle.SavedStateHandle
import com.example.customerclient.common.Constants
import com.example.customerclient.data.repository.BillRepositoryImpl
import com.example.customerclient.data.repository.CreditRepositoryImpl
import com.example.customerclient.data.repository.ExchangeRateRepositoryImpl
import com.example.customerclient.data.repository.UserRepositoryImpl
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.domain.repositories.ExchangeRateRepository
import com.example.customerclient.domain.repositories.UserRepository
import com.example.customerclient.domain.usecases.bill.GetBillInfoUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.usecases.user.GetUserNameUseCase
import com.example.customerclient.ui.auth.signin.SignInViewModel
import com.example.customerclient.ui.bill.all.AllBillsViewModel
import com.example.customerclient.ui.bill.info.BillInfoViewModel
import com.example.customerclient.ui.bottombar.home.HomeViewModel
import com.example.customerclient.ui.credit.all.AllCreditsViewModel
import com.example.customerclient.ui.credit.info.CreditInfoViewModel
import okhttp3.OkHttpClient
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import java.util.concurrent.TimeUnit

val appModule = module {

    factory { provideRetrofit() }

    // region : Repositories
    single<BillRepository> { BillRepositoryImpl() }
    single<CreditRepository> { CreditRepositoryImpl() }
    single<ExchangeRateRepository> { ExchangeRateRepositoryImpl() }
    single<UserRepository> { UserRepositoryImpl() }
    // end region

    // region : Use Cases
    // - Bill
    single<GetUserBillsInfoUseCase> { GetUserBillsInfoUseCase(billRepository = get()) }
    single<GetBillInfoUseCase> { GetBillInfoUseCase(billRepository = get()) }

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

    viewModel { AllBillsViewModel(getUserBillsInfoUseCase = get()) }
    viewModel { (handle: SavedStateHandle) ->
        BillInfoViewModel(
            handle,
            getBillInfoUseCase = get()
        )
    }

    viewModel { (handle: SavedStateHandle) -> CreditInfoViewModel(handle) }
    viewModel { AllCreditsViewModel() }
    //end region
}

fun provideRetrofit(): Retrofit {
    return Retrofit.Builder()
        .baseUrl(Constants.BASE_URL)
        .addConverterFactory(GsonConverterFactory.create())
        .client(
            OkHttpClient.Builder()
                .readTimeout(60, TimeUnit.SECONDS)
                .connectTimeout(60, TimeUnit.SECONDS)
                .build()
        )
        .build()
}