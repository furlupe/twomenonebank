package com.example.employeeclient.data.remote.api

import com.example.employeeclient.data.remote.dto.credit.CreditDetailsDto
import com.example.employeeclient.data.remote.dto.credit.CreditsPageDto
import com.example.employeeclient.data.remote.dto.credit.operation.CreditOperationsPageDto
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface CreditApi {

    @GET("api/manage/credits/of/{userId}")
    suspend fun getAllUserCredits(
        @Path("userId") userId: String,
        @Query("page") page: Int = 1
    ): CreditsPageDto

    @GET("api/manage/credits/{creditId}")
    suspend fun getCreditDetails(
        @Path("creditId") creditId: String
    ): CreditDetailsDto

    @GET("api/manage/credits/{creditId}/operations")
    suspend fun getAllCreditOperations(
        @Path("creditId") creditId: String,
        @Query("page") page: Int = 1
    ): CreditOperationsPageDto

}