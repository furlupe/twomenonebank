package com.example.employeeclient.data.remote.api

import com.example.employeeclient.data.remote.dto.user.RegisterInfoDto
import com.example.employeeclient.data.remote.dto.user.UserDto
import com.example.employeeclient.data.remote.dto.user.UsersPageDto
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface UserApi {
    @POST("api/User/register")
    suspend fun register(
        @Body info: RegisterInfoDto
    )

    @POST("api/User/{userId}/ban")
    suspend fun banUserById(
        @Path("userId") id: String
    )

    @POST("api/User/{userId}/unban")
    suspend fun unbanUserById(
        @Path("userId") id: String
    )

    @GET("api/User")
    suspend fun getUsers(
        @Query("page") page: Int
    ): UsersPageDto

    @GET("api/User/{userId}")
    suspend fun getUser(
        @Path("userId") userId: String
    ): UserDto

    @GET("api/User/me")
    suspend fun me(): UserDto
}