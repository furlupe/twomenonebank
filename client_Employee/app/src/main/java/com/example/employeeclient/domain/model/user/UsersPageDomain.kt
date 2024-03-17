package com.example.employeeclient.domain.model.user

data class UsersPageDomain(
    val currentPage: Int,
    val totalPages: Int,
    val users: List<UserDomain>
)
