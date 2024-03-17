package com.example.employeeclient.data.remote.dto.account

import com.example.employeeclient.data.remote.dto.common.PageInfoDto

data class AccountsPageDto(
    val items: List<AccountDto>?,
    val pageInfo: PageInfoDto
)