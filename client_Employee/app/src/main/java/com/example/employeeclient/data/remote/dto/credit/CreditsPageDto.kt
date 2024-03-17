package com.example.employeeclient.data.remote.dto.credit

import com.example.employeeclient.data.remote.dto.common.PageInfoDto
import com.example.employeeclient.data.remote.dto.credit.CreditDto

data class CreditsPageDto(
    val items: List<CreditDto>?,
    val pageInfo: PageInfoDto
)