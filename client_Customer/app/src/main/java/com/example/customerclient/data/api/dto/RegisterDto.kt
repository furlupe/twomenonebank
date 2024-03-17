package com.example.customerclient.data.api.dto

data class RegisterDto(
    val email: String,
    val password: String,
    val userName: String,
    val role: Int = 2
)
