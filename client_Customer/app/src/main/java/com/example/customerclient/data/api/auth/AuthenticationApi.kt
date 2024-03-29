package com.example.customerclient.data.api.auth

import com.example.customerclient.data.api.dto.TokenDto
import retrofit2.http.Field
import retrofit2.http.FormUrlEncoded
import retrofit2.http.GET
import retrofit2.http.Headers
import retrofit2.http.POST
import retrofit2.http.Query

interface AuthenticationApi {
    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/token")
    suspend fun connect(
        @Field("grant_type") grantType: String = "authorization_code",
        @Field("client_id") clientId: String = "amogus",
        @Field("code") code: String,
        @Field("redirect_uri") redirectUri: String = "twomenonebank://customer.com",
    ): TokenDto


    @GET("connect/authorize")
    suspend fun authorize(
        @Query("client_id") clientId: String = "amogus",
        @Query("response_type") responseType: String = "code",
        @Query("redirect_uri") redirectUri: String = "twomenonebank://customer.com",
        @Query("scope") scope: String = "offline_access",
    )

    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/token")
    suspend fun connect(
        @Field("grant_type") grantType: String = "refresh_token",
        @Field("refresh_token") refreshToken: String,
        @Field("client_id") clientId: String = "amogus",
    ): TokenDto
}