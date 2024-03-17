package com.example.employeeclient.presentation.main.credit

data class CreateTariffState(
    val isLoading: Boolean = false,
    val created: Boolean = false,
    val error: String = ""
)
