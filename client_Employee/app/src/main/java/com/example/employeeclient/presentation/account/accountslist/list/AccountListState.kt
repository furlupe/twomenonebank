package com.example.employeeclient.presentation.account.accountslist.list

import com.example.employeeclient.domain.model.account.AccountDomain

data class AccountListState(
    val userId: String,
    val userName: String = "Undefined",
    val isBanned: Boolean = false,
    val currentPage: Int = 1,
    val totalPages: Int = 1,
    val items: List<AccountDomain> = emptyList(),
    val isLastPage: Boolean = true,
)
