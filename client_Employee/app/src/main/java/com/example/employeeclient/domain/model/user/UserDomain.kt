package com.example.employeeclient.domain.model.user

import com.example.employeeclient.domain.model.enums.Role

data class UserDomain(
    val id: String,
    val name: String = "",
    val email: String = "",
    val role: Role = Role.User,
    val isBanned: Boolean = false
)
