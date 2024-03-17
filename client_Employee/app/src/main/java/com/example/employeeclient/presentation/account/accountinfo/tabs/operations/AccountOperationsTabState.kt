package com.example.employeeclient.presentation.account.accountinfo.tabs.operations

import com.example.employeeclient.domain.model.account.event.AccountEventDomain

data class AccountOperationsTabState(
    val currentPage: Int = 1,
    val totalPages: Int = 1,
    val items: List<AccountEventDomain> = emptyList(),
    val isLastPage: Boolean = true
)
