package com.example.employeeclient.auth.signin


sealed class SignInState {
    data class Idle (val error: String? = null) : SignInState()
    data object Loading : SignInState()
}