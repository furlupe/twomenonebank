package com.example.employeeclient.presentation.account.accountinfo.tabs.details

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.model.account.AccountDomain
import com.example.employeeclient.domain.repository.remote.AccountRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class AccountDetailsTabViewModel(
    accountId: String,
    private val accountRepository: AccountRepository
): ViewModel() {
    private val _state = MutableStateFlow<AccountDomain?>(null)
    val state: StateFlow<AccountDomain?> = _state

    init {
        getCreditDetails(accountId)
    }

    private fun getCreditDetails(creditId: String) = viewModelScope.launch {
        val details = accountRepository.getAccount(creditId)

        _state.update { details }
    }
}