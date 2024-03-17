package com.example.employeeclient.data.remote.dto.user

import com.example.employeeclient.data.remote.dto.common.PageInfoDto

data class UsersPageDto(
    val items: List<UserDto>?,
    val pageInfo: PageInfoDto
)