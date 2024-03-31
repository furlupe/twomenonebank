package com.example.employeeclient.domain.model.user

data class RegisterInfoDomain(
    val username: String,
    val email: String,
    val password: String,
    val phone: String,
    val roles: List<Int>
)
