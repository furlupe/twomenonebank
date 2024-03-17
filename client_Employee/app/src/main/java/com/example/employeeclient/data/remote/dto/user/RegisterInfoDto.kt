package com.example.employeeclient.data.remote.dto.user

data class RegisterInfoDto(
    val username: String,
    val email: String,
    val password: String,
    val role: Int
)