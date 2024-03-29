package com.example.employeeclient.data.remote.api

import com.example.employeeclient.data.remote.dto.TokenDto
import retrofit2.http.Field
import retrofit2.http.FormUrlEncoded
import retrofit2.http.Headers
import retrofit2.http.POST

interface AuthApi {

    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/authorize")
    suspend fun authorize(
        @Field("client_id") clientId: String = "amogus",
        @Field("response_type") responseType: String = "code",
        @Field("redirect_uri") redirectUri: String = "myschema://twomenonebank.com/employee",
        @Field("scope") scope: String = "offline_access",
    )

    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/token")
    suspend fun connect(
        @Field("grant_type") grantType: String = "authorization_code",
        @Field("code") code: String,
        @Field("client_id") clientId: String = "amogus",
        @Field("redirect_uri") redirectUri: String = "myschema://twomenonebank.com/employee",
    ): TokenDto

    @FormUrlEncoded
    @Headers("Content-Type:application/x-www-form-urlencoded")
    @POST("connect/token")
    suspend fun connectRefresh(
        @Field("grant_type") grantType: String = "refresh_token",
        @Field("refresh_token") refreshToken: String,
        @Field("client_id") clientId: String = "amogus",
    ): TokenDto

}