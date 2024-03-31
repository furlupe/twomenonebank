package com.example.employeeclient.data.remote.dto.account.event

data class RequestAccountHistoryBodyDto(
    val name: String = "",
    val pageNumber: Int = 1,
    val pageSize: Int = 20,
    val sortingType: String = "Ascending",
    val from: String? = null,
    val to: String? = null
)