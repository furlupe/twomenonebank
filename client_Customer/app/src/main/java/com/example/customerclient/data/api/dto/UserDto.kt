package com.example.customerclient.data.api.dto

import com.example.customerclient.domain.models.UserInfo

data class UserDto(
    val id: String,
    val email: String?,
    val name: String?,
    val role: Int,
    val isBanned: Boolean
)

fun UserDto.toUserInfo(): UserInfo = UserInfo(id = this.id, name = this.name ?: "")
