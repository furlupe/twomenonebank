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
import com.example.employeeclient.domain.model.account.event.BalanceDomain
import com.example.employeeclient.domain.model.account.event.TransferDomain
import com.example.employeeclient.domain.model.enums.AccountEventState
import com.example.employeeclient.domain.model.enums.AccountEventType
import com.example.employeeclient.domain.model.enums.BalanceChangeType
import com.example.employeeclient.domain.model.enums.Currency

fun BalanceDto.toDomain() = BalanceDomain(
    amount = amount,
    currency = Currency.fromString(currency)
)

fun AccountDto.toDomain() = AccountDomain(
    id = id,
    balance = balance.toDomain(),
    name = name ?: "Unnamed",
)

fun AccountsPageDto.toDomain() = AccountsPageDomain(
    currentPage = pageInfo.currentPage,
    totalPages = pageInfo.totalPages,
    items = items?.map { it.toDomain() } ?: emptyList()
)

fun BalanceChangeDto.toDomain() = BalanceChangeDomain(
    nativeValue = nativeValue.toDomain(),
    foreignValue = foreignValue.toDomain(),
    accountId = accountId,
    eventType = eventType?.let { BalanceChangeType.valueOf(it) } ?: BalanceChangeType.Deposit,
)

fun TransferDto.toDomain() = TransferDomain(
    id = id,
    source = source.toDomain(),
    target = target.toDomain()
)

fun AccountEventDto.toDomain() = AccountEventDomain(
    id = id,
    comment = comment ?: "No comment",
    eventType = AccountEventType.fromString(eventType),
    balanceChange = balanceChange?.toDomain(),
    transfer = transfer?.toDomain(),
    resolvedAt = resolvedAt.parseDateToReadable(),
    state = AccountEventState.valueOf(state)
)

fun AccountEventsPageDto.toDomain() = AccountEventsPageDomain(
    currentPage = pageInfo.currentPage,
    totalPages = pageInfo.totalPages,
    items = items?.map { it.toDomain() } ?: emptyList()
)
