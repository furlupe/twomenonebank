package com.example.employeeclient.data.remote.dto

import com.example.employeeclient.common.Constants

data class ConnectBodyDto(
    val code: String,
    val redirectUri: String = Constants.DEEP_LINK
)
