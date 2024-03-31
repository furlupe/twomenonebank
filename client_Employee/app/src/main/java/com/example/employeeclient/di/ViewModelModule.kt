package com.example.employeeclient.di

import com.example.employeeclient.presentation.account.accountinfo.tabs.details.AccountDetailsTabViewModel
import com.example.employeeclient.presentation.account.accountinfo.tabs.operations.AccountOperationsTabViewModel
import com.example.employeeclient.presentation.account.accountslist.list.AccountListViewModel
import com.example.employeeclient.presentation.auth.signin.SignInViewModel
import com.example.employeeclient.presentation.credit.creditinfo.tabs.details.CreditDetailsTabViewModel
import com.example.employeeclient.presentation.credit.creditinfo.tabs.operations.CreditOperationsTabViewModel
import com.example.employeeclient.presentation.credit.creditslist.list.CreditsListViewModel
import com.example.employeeclient.presentation.main.adduser.AddUserViewModel
import com.example.employeeclient.presentation.main.credit.CreditViewModel
import com.example.employeeclient.presentation.main.sync.SyncViewModel
import com.example.employeeclient.presentation.main.users.UsersViewModel
import org.koin.androidx.viewmodel.dsl.viewModel
import org.koin.dsl.module

val viewModelModule = module {
    includes(appModule)
    includes(useCaseModule)

    viewModel { SignInViewModel(get(), get()) }
    viewModel { UsersViewModel(get(), get()) }
    viewModel { (userId: String) -> CreditsListViewModel(userId, get(), get(), get(), get(), get()) }
    viewModel { (creditId: String) -> CreditDetailsTabViewModel(creditId, get()) }
    viewModel { (userId: String) -> CreditOperationsTabViewModel(userId, get()) }
    viewModel { AddUserViewModel(get(), get()) }
    viewModel { CreditViewModel(get(), get()) }
    viewModel { AccountListViewModel(get(), get(), get(), get(), get(), get()) }
    viewModel { (creditId: String) -> AccountOperationsTabViewModel(creditId, get()) }
    viewModel { (creditId: String) -> AccountDetailsTabViewModel(creditId, get()) }
    viewModel { SyncViewModel(get()) }
}