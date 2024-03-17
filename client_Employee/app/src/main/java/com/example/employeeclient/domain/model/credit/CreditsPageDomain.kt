package com.example.employeeclient.domain.model.credit

data class CreditsPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val items: List<CreditDomain>
)