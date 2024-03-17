package com.example.employeeclient.domain.model.account.event

data class AccountEventsPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val items: List<AccountEventDomain>
)