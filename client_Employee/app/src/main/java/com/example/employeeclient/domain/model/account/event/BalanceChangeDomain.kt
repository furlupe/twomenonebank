package com.example.employeeclient.domain.model.account.event

import com.example.employeeclient.domain.model.enums.BalanceChangeType

data class BalanceChangeDomain(
    val value: Int = 0,
    val accountId: String = "-1",
    val accountName: String = "Undefined",
    val eventType: BalanceChangeType = BalanceChangeType.Deposit,
)