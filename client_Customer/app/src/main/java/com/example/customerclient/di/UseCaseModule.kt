package com.example.customerclient.di

import com.example.customerclient.domain.usecases.auth.AuthorizeUseCase
import com.example.customerclient.domain.usecases.auth.SignInUseCase
import com.example.customerclient.domain.usecases.bill.CloseBillUseCase
import com.example.customerclient.domain.usecases.bill.DepositUseCase
import com.example.customerclient.domain.usecases.bill.GetBillHistoryUseCase
import com.example.customerclient.domain.usecases.bill.GetBillInfoUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsPagedInfoUseCase
import com.example.customerclient.domain.usecases.bill.OpenBillUseCase
import com.example.customerclient.domain.usecases.bill.SaveUserBillInfoToDatabaseUseCase
import com.example.customerclient.domain.usecases.bill.WithdrawUseCase
import com.example.customerclient.domain.usecases.credit.CreateCreditUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditHistoryUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditTariffsUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditRateUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoPagingUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.credit.PayCreditUseCase
import com.example.customerclient.domain.usecases.credit.SaveUserCreditInfoToDatabaseUseCase
import com.example.customerclient.domain.usecases.transaction.Me2MeTransactionUseCase
import com.example.customerclient.domain.usecases.transaction.P2PTransactionUseCase
import com.example.customerclient.domain.usecases.user.GetUserInfoUseCase
import com.example.customerclient.domain.usecases.user.GetUserThemeUseCase
import org.koin.dsl.module

val useCaseModule = module {
    includes(appModule)

    // - Auth
    single<SignInUseCase> { SignInUseCase(authRepository = get()) }
    single<AuthorizeUseCase> { AuthorizeUseCase(authRepository = get()) }

    // - Bill
    single<GetUserBillsInfoUseCase> { GetUserBillsInfoUseCase(billRepository = get()) }
    single<GetBillInfoUseCase> { GetBillInfoUseCase(billRepository = get()) }
    single<GetUserBillsPagedInfoUseCase> { GetUserBillsPagedInfoUseCase(billRepository = get()) }
    single<GetBillHistoryUseCase> { GetBillHistoryUseCase(billRepository = get()) }
    single<OpenBillUseCase> { OpenBillUseCase(billRepository = get()) }
    single<CloseBillUseCase> { CloseBillUseCase(billRepository = get()) }
    single<DepositUseCase> { DepositUseCase(transactionRepository = get()) }
    single<WithdrawUseCase> { WithdrawUseCase(transactionRepository = get()) }

    single<GetUserBillsInfoFromDatabaseUseCase> { GetUserBillsInfoFromDatabaseUseCase(billRepository = get()) }
    single<SaveUserBillInfoToDatabaseUseCase> { SaveUserBillInfoToDatabaseUseCase(billRepository = get()) }

    // - Credit
    single<GetUserCreditsInfoPagingUseCase> { GetUserCreditsInfoPagingUseCase(creditRepository = get()) }
    single<GetUserCreditsInfoUseCase> { GetUserCreditsInfoUseCase(creditRepository = get()) }
    single<GetCreditInfoUseCase> { GetCreditInfoUseCase(creditRepository = get()) }
    single<GetCreditHistoryUseCase> { GetCreditHistoryUseCase(creditRepository = get()) }
    single<CreateCreditUseCase> { CreateCreditUseCase(creditRepository = get()) }
    single<GetCreditTariffsUseCase> { GetCreditTariffsUseCase(creditRepository = get()) }
    single<PayCreditUseCase> { PayCreditUseCase(creditRepository = get()) }

    single<GetUserCreditsInfoFromDatabaseUseCase> {
        GetUserCreditsInfoFromDatabaseUseCase(
            creditRepository = get()
        )
    }
    single<SaveUserCreditInfoToDatabaseUseCase> {
        SaveUserCreditInfoToDatabaseUseCase(
            creditRepository = get()
        )
    }
    single<GetUserCreditRateUseCase> { GetUserCreditRateUseCase(creditRepository = get()) }

    // - User
    single<GetUserInfoUseCase> { GetUserInfoUseCase(userRepository = get()) }
    single<GetUserThemeUseCase> { GetUserThemeUseCase(sharedPreferencesRepositoryImpl = get()) }

    // - Transaction
    single<P2PTransactionUseCase> { P2PTransactionUseCase(transactionRepository = get()) }
    single<Me2MeTransactionUseCase> { Me2MeTransactionUseCase(transactionRepository = get()) }
}
