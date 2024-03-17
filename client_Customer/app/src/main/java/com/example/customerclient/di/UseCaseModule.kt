package com.example.customerclient.di

import com.example.customerclient.domain.usecases.auth.SignInUseCase
import com.example.customerclient.domain.usecases.bill.GetBillInfoUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.credit.CreateCreditUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditHistoryUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditTariffsUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoPagingUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.credit.PayCreditUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.usecases.user.GetUserInfoUseCase
import org.koin.dsl.module

val useCaseModule = module {
    includes(appModule)

    // - Auth
    single<SignInUseCase> { SignInUseCase(authRepository = get()) }

    // - Bill
    single<GetUserBillsInfoUseCase> { GetUserBillsInfoUseCase(billRepository = get()) }
    single<GetBillInfoUseCase> { GetBillInfoUseCase(billRepository = get()) }

    // - Credit
    single<GetUserCreditsInfoPagingUseCase> { GetUserCreditsInfoPagingUseCase(creditRepository = get()) }
    single<GetUserCreditsInfoUseCase> { GetUserCreditsInfoUseCase(creditRepository = get()) }
    single<GetCreditInfoUseCase> { GetCreditInfoUseCase(creditRepository = get()) }
    single<GetCreditHistoryUseCase> { GetCreditHistoryUseCase(creditRepository = get()) }
    single<CreateCreditUseCase> { CreateCreditUseCase(creditRepository = get()) }
    single<GetCreditTariffsUseCase> { GetCreditTariffsUseCase(creditRepository = get()) }
    single<PayCreditUseCase> { PayCreditUseCase(creditRepository = get()) }

    // - ExchangeRate
    single<GetDollarExchangeRateUseCase> { GetDollarExchangeRateUseCase(exchangeRateRepository = get()) }
    single<GetEuroExchangeRateUseCase> { GetEuroExchangeRateUseCase(exchangeRateRepository = get()) }

    // - User
    single<GetUserInfoUseCase> { GetUserInfoUseCase(userRepository = get()) }

}
