package com.example.employeeclient.data.remote.dto.account

import com.example.employeeclient.common.parseDateToReadable
import com.example.employeeclient.data.remote.dto.account.event.AccountEventDto
import com.example.employeeclient.data.remote.dto.account.event.AccountEventsPageDto
import com.example.employeeclient.data.remote.dto.account.event.BalanceChangeDto
import com.example.employeeclient.data.remote.dto.account.event.TransferDto
import com.example.employeeclient.domain.model.account.AccountDomain
import com.example.employeeclient.domain.model.account.AccountsPageDomain
import com.example.employeeclient.domain.model.account.event.AccountEventDomain
import com.example.employeeclient.domain.model.account.event.AccountEventsPageDomain
import com.example.employeeclient.domain.model.account.event.BalanceChangeDomain
import com.example.employeeclient.domain.model.account.event.TransferDomain
import com.example.employeeclient.domain.model.enums.AccountEventState
import com.example.employeeclient.domain.model.enums.AccountEventType
import com.example.employeeclient.domain.model.enums.BalanceChangeType

fun AccountDto.toDomain() = AccountDomain(
    id = this.id,
    balance = this.balance,
    name = this.name ?: "Unnamed",
)

fun AccountsPageDto.toDomain() = AccountsPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    items = this.items?.map { it.toDomain() } ?: emptyList()
)

fun BalanceChangeDto.toDomain() = BalanceChangeDomain(
    value = this.value,
    accountId = this.accountId,
    eventType = BalanceChangeType.valueOf(this.eventType)
)

fun TransferDto.toDomain() = TransferDomain(
    id = this.id,
    source = this.source.toDomain(),
    target = this.target.toDomain()
)

fun AccountEventDto.toDomain() = AccountEventDomain(
    id = this.id,
    comment = this.comment ?: "No comment",
    eventType = AccountEventType.fromString(this.eventType),
    balanceChange = this.balanceChange?.toDomain(),
    transfer = this.transfer?.toDomain(),
    resolvedAt = this.resolvedAt.parseDateToReadable(),
    state = AccountEventState.valueOf(this.state)
)

fun AccountEventsPageDto.toDomain() = AccountEventsPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    items = this.items?.map { it.toDomain() } ?: emptyList()
)
