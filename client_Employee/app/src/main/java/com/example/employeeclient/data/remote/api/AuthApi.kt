package com.example.employeeclient.data.remote.api

import com.example.employeeclient.common.Constants.DEEP_LINK
import com.example.employeeclient.data.remote.dto.ConnectBodyDto
import com.example.employeeclient.data.remote.dto.IsDarkThemeDto
import com.example.employeeclient.data.remote.dto.RefreshTokenDto
import com.example.employeeclient.data.remote.dto.TokenDto
import retrofit2.http.Body
import retrofit2.http.FormUrlEncoded
import retrofit2.http.GET
import retrofit2.http.Headers
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface AuthApi {

    @GET("connect/authorize")
    suspend fun authorize(
        @Query("redirectUri") redirectUri: String = DEEP_LINK
    )

    @POST("connect/token")
    suspend fun connect(
        @Body body: ConnectBodyDto,
    ): TokenDto

    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/refresh")
    suspend fun connectRefresh(
        @Body refreshToken: RefreshTokenDto,
    ): TokenDto

    @POST("theme/{isDark}")
    suspend fun updateUserTheme(
        @Path("isDark") isDark: Boolean
    )

    @GET("theme")
    suspend fun getIsDarkTheme(): IsDarkThemeDto

}
