package com.example.employeeclient.data.remote.dto.account.event

data class RequestAccountHistoryBodyDto(
    val pageNumber: Int,
    val pageSize: Int,
    val sortingType: String,
    val from: String?,
    val to: String?
)