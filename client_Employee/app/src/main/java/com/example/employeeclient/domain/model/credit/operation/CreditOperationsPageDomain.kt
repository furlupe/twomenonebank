package com.example.employeeclient.domain.model.credit.operation

data class CreditOperationsPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val items: List<CreditOperationDomain>
)