package com.example.employeeclient.presentation.main.users

import com.example.employeeclient.domain.model.user.UserDomain

data class UsersState(
    val users: List<UserDomain> = emptyList(),
    val currentPage: Int = 1,
    val isPageLoading: Boolean = false,
    val isLastPage: Boolean = false,
    val error: String = "",
    val isLoading: Boolean = true
)
