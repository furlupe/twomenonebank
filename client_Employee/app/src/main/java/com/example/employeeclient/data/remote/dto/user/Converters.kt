package com.example.employeeclient.data.remote.dto.user

import com.example.employeeclient.domain.model.enums.Role
import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.model.user.UserDomain
import com.example.employeeclient.domain.model.user.UsersPageDomain

fun UserDto.toDomain() = UserDomain(
    id = id,
    name = name ?: "Unnamed",
    email = email,
    roles = roles.map { Role.fromInt(it) },
    isBanned = isBanned,
    creditRating = creditRating
)

fun UsersPageDto.toDomain() = UsersPageDomain(
    currentPage = pageInfo.currentPage,
    totalPages = pageInfo.totalPages,
    users = items?.map { it.toDomain() } ?: emptyList()
)

fun RegisterInfoDomain.toDto() = RegisterInfoDto(
    username = username,
    email = email,
    password = password,
    phone = phone,
    roles = roles
)
