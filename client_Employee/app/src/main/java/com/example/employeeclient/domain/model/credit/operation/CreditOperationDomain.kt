package com.example.employeeclient.domain.model.credit.operation

import com.example.employeeclient.domain.model.enums.CreditOperationType

data class CreditOperationDomain(
    val id: String,
    val creditId: String = "",
    val type: CreditOperationType = CreditOperationType.Closed,
    val happenedAt: String = "",
    val to: String = "",
    val amount: Int? = null,
)