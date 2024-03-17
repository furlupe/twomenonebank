package com.example.customerclient.data.api.dto

data class TokenDto(
    val access_token: String,
    val token_type: String,
    val expires_in: Long,
    val refresh_token: String
)
