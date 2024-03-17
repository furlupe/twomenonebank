package com.example.customerclient.data.api.auth

import com.example.customerclient.data.api.dto.RegisterDto
import com.example.customerclient.data.api.dto.UserDto
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface UserApi {
    @GET("api/User/me")
    suspend fun getUserInfo(): UserDto

    @POST("api/User/register")
    suspend fun registerUser(@Body registerBody: RegisterDto)
}