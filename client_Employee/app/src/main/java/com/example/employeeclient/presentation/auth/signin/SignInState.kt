package com.example.employeeclient.presentation.auth.signin

data class SignInState(
    val isLoading: Boolean = false,
    val error: String = "",
    val redirectLink: String? = null,
    val navigateToMain: Boolean = false
)