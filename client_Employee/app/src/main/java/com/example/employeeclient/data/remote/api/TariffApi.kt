package com.example.employeeclient.data.remote.api

import com.example.employeeclient.data.remote.dto.tariff.TariffCreateDto
import com.example.employeeclient.data.remote.dto.tariff.TariffsPageDto
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Query

interface TariffApi {

    @GET("api/manage/Tariff")
    suspend fun getAllTariffs(
        @Query("page") page: Int = 1
    ): TariffsPageDto

    @POST("api/manage/Tariff")
    suspend fun createTariff(
        @Body body: TariffCreateDto
    )

}