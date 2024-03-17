package com.example.customerclient.data.api.core

import com.example.customerclient.data.api.dto.AccountCreateDto
import com.example.customerclient.data.api.dto.AccountDto
import com.example.customerclient.data.api.dto.AccountEventPageDto
import com.example.customerclient.data.api.dto.AccountPageDto
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface AccountsApi {
    @GET("accounts/my")
    suspend fun getUserBills(
        @Query("PageNumber") pageNumber: Int,
        @Query("PageSize") pageSize: Int,
        @Query("SortingType") sortingType: String = "Ascending",
    ): AccountPageDto

    @GET("accounts/{id}")
    suspend fun getBillInfo(@Path("id") billId: String): AccountDto

    @GET("accounts/{id}/history")
    suspend fun getBillHistory(
        @Path("id") id: String,
        @Query("PageNumber") pageNumber: Int,
        @Query("PageSize") pageSize: Int,
        @Query("SortingType") sortingType: String = "Ascending",
    ): AccountEventPageDto

    @POST("accounts/open")
    suspend fun openBill(@Body billCreateBody: AccountCreateDto): String

    @DELETE("accounts/close/{id}")
    suspend fun closeBill(@Path("id") billId: String)

}