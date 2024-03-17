package com.example.employeeclient.domain.model.account

data class AccountsPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val items: List<AccountDomain>,
)