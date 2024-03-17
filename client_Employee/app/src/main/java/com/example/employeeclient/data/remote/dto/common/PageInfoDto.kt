package com.example.employeeclient.data.remote.dto.common

data class PageInfoDto(
    val currentPage: Int,
    val totalItems: Int,
    val totalPages: Int
)