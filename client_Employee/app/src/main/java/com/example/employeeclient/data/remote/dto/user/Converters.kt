package com.example.employeeclient.data.remote.dto.user

import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.model.enums.Role
import com.example.employeeclient.domain.model.user.UserDomain
import com.example.employeeclient.domain.model.user.UsersPageDomain

fun UserDto.toDomain() = UserDomain(
    id = this.id,
    name = this.name ?: "Unnamed",
    email = this.email,
    role = Role.fromInt(this.role),
    isBanned = this.isBanned
)

fun UsersPageDto.toDomain() = UsersPageDomain(
    currentPage = this.pageInfo.currentPage,
    totalPages = this.pageInfo.totalPages,
    users = this.items?.map { it.toDomain() } ?: emptyList()
)

fun RegisterInfoDomain.toDto() = RegisterInfoDto(
    username = this.username,
    email = this.email,
    password = this.password,
    role = this.role
)
