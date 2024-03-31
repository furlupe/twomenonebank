package com.example.employeeclient.domain.model.account

import com.example.employeeclient.domain.model.account.event.BalanceDomain

data class AccountDomain(
    val id: String,
    val balance: BalanceDomain = BalanceDomain(),
    val name: String = "Unnamed",
    val isDefault: Boolean = false,
    val isClosed: Boolean = false
)
