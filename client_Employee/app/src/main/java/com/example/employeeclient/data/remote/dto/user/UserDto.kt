package com.example.employeeclient.data.remote.dto.user

data class UserDto(
    val name: String?,
    val email: String,
    val id: String,
    val role: Int,
    val isBanned: Boolean
)