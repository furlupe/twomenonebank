package com.example.employeeclient.data.remote.dto.account.event

import com.example.employeeclient.data.remote.dto.common.PageInfoDto

data class AccountEventsPageDto(
    val items: List<AccountEventDto>?,
    val pageInfo: PageInfoDto
)