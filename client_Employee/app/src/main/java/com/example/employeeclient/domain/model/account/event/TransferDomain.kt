package com.example.employeeclient.domain.model.account.event

data class TransferDomain(
    val id: String? = "-1",
    val source: BalanceChangeDomain = BalanceChangeDomain(),
    val target: BalanceChangeDomain = BalanceChangeDomain()
)