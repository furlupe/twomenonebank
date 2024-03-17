package com.example.employeeclient.domain.model.account.event

import com.example.employeeclient.domain.model.enums.AccountEventState
import com.example.employeeclient.domain.model.enums.AccountEventType

data class AccountEventDomain(
    val id: String,
    val comment: String = "",
    val eventType: AccountEventType = AccountEventType.BalanceChange,
    val balanceChange: BalanceChangeDomain? = null,
    val transfer: TransferDomain? = null,
    val resolvedAt: String = "",
    val state: AccountEventState = AccountEventState.Canceled
)