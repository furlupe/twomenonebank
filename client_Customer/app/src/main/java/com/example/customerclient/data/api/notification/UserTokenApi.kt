package com.example.customerclient.data.api.notification

import com.example.customerclient.data.api.dto.UserTokenDto
import retrofit2.http.Body
import retrofit2.http.POST

interface UserTokenApi {
    @POST("api/User")
    suspend fun createNotification(@Body userTokenDto: UserTokenDto)
}