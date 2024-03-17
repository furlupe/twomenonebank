package com.example.customerclient.data.api.credit

import com.example.customerclient.data.api.dto.CreateCreditDto
import com.example.customerclient.data.api.dto.CreditDto
import com.example.customerclient.data.api.dto.CreditOperationPageDto
import com.example.customerclient.data.api.dto.CreditsPageDto
import com.example.customerclient.data.api.dto.TariffsPageDto
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface CreditsApi {
    @GET("api/Credit/my")
    suspend fun getUserCredits(@Query("page") page: Int): CreditsPageDto

    @GET("api/Credit/my/{creditId}")
    suspend fun getCreditInfo(@Path("creditId") creditId: String): CreditDto

    @GET("api/Credit/my/{creditId}/operations")
    suspend fun getCreditHistory(
        @Path("creditId") creditId: String,
        @Query("page") page: Int
    ): CreditOperationPageDto

    @POST("api/Credit")
    suspend fun createCredit(@Body createCreditBody: CreateCreditDto)

    @POST("api/Credit/{creditId}/pay")
    suspend fun payCredit(@Path("creditId") creditId: String)

    @GET("api/manage/Tariff")
    suspend fun getCreditsTariffs(@Query("page") page: Int): TariffsPageDto
}