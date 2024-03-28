package com.example.employeeclient.data.remote.dto.user

data class UserDto(
    val id: String,
    val email: String,
    val name: String?,
    val roles: List<Int>,
    val phone: String,
    val isBanned: Boolean
)