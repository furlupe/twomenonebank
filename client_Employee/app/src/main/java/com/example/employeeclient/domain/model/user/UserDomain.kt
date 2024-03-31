package com.example.employeeclient.domain.model.user

import com.example.employeeclient.domain.model.enums.Role

data class UserDomain(
    val id: String,
    val name: String = "",
    val email: String = "",
    val roles: List<Role> = listOf(Role.User),
    val isBanned: Boolean = false,
    val creditRating: Int = 0
)
