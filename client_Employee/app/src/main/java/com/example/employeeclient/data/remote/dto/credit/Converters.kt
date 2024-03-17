package com.example.employeeclient.data.remote.dto.credit

import com.example.employeeclient.common.parseDateToReadable
import com.example.employeeclient.data.remote.dto.credit.operation.CreditOperationDto
import com.example.employeeclient.data.remote.dto.credit.operation.CreditOperationsPageDto
import com.example.employeeclient.data.remote.dto.tariff.toDomain
import com.example.employeeclient.domain.model.credit.CreditDetailsDomain
import com.example.employeeclient.domain.model.credit.CreditDomain
import com.example.employeeclient.domain.model.credit.CreditsPageDomain
import com.example.employeeclient.domain.model.credit.operation.CreditOperationDomain
import com.example.employeeclient.domain.model.credit.operation.CreditOperationsPageDomain
import com.example.employeeclient.domain.model.enums.CreditOperationType

fun CreditDto.toDomain() = CreditDomain(
    id = this.id,
    amount = this.amount,
    tariff = this.tariff ?: "Unnamed",
    days = this.days,
    isClosed = this.isClosed
)

fun CreditDetailsDto.toDomain() = CreditDetailsDomain(
    id = this.id,
    tariff = this.tariff.toDomain(),
    amount = amount,
    baseAmount = baseAmount,
    days = days,
    penalty = penalty,
    periodicPayment,
    isClosed
)

fun CreditsPageDto.toDomain() = CreditsPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    items = this.items?.map { it.toDomain() } ?: emptyList()
)

fun CreditOperationDto.toDomain() = CreditOperationDomain(
    id = this.id,
    creditId = creditId,
    type = CreditOperationType.fromInt(this.type),
    happenedAt = happenedAt.parseDateToReadable(),
    amount = amount
)

fun CreditOperationsPageDto.toDomain() = CreditOperationsPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    items = this.items?.map { it.toDomain() } ?: emptyList()
)
