package com.example.employeeclient.data.remote.dto.credit.operation

import com.example.employeeclient.data.remote.dto.common.PageInfoDto

data class CreditOperationsPageDto(
    val items: List<CreditOperationDto>?,
    val pageInfo: PageInfoDto
)