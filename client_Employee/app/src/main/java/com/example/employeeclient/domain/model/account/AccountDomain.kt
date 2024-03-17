package com.example.employeeclient.domain.model.account

data class AccountDomain(
    val id: String,
    val balance: Int = 0,
    val name: String = "Unnamed"
)
