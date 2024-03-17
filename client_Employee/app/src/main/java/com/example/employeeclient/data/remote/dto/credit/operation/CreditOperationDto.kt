package com.example.employeeclient.data.remote.dto.credit.operation

data class CreditOperationDto(
    val id: String,
    val creditId: String,
    val type: Int,
    val happenedAt: String,
    val to: String?,
    val amount: Int?
)