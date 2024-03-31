package com.example.employeeclient.presentation.credit.creditslist.list

import com.example.employeeclient.domain.model.credit.CreditDomain

data class CreditListState(
    val userId: String,
    val userName: String = "Undefined",
    val userCreditRating: Int = 0,
    val isBanned: Boolean = false,
    val currentPage: Int = 1,
    val totalPages: Int = 1,
    val items: List<CreditDomain> = emptyList(),
    val isLastPage: Boolean = true,
)
