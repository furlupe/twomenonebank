package com.example.employeeclient.presentation.auth.signin

data class SignInState(
    val isLoading: Boolean = false,
    val error: String = "",
    val navigateToMain: Boolean = false
)