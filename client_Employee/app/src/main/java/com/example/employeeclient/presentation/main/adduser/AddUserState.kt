package com.example.employeeclient.presentation.main.adduser

data class AddUserState(
    val isLoading: Boolean = false,
    val registered: Boolean = false,
    val error: String = ""
)
