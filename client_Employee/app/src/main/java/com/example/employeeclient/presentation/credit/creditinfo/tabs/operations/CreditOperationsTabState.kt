package com.example.employeeclient.presentation.credit.creditinfo.tabs.operations

import com.example.employeeclient.domain.model.credit.operation.CreditOperationDomain

data class CreditOperationsTabState(
    val currentPage: Int = 1,
    val totalPages: Int = 1,
    val items: List<CreditOperationDomain> = emptyList(),
    val isLastPage: Boolean = true
)
