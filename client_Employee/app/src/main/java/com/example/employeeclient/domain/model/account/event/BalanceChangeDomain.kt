package com.example.employeeclient.domain.model.account.event

import com.example.employeeclient.domain.model.enums.BalanceChangeType

data class BalanceChangeDomain(
    val nativeValue: BalanceDomain = BalanceDomain(),
    val foreignValue: BalanceDomain = BalanceDomain(),
    val accountId: String = "-1",
    val eventType: BalanceChangeType = BalanceChangeType.Deposit,
    val creditId: String? = null
)