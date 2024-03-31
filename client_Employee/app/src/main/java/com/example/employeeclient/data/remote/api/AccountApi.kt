package com.example.employeeclient.data.remote.api

import com.example.employeeclient.data.remote.dto.account.AccountDto
import com.example.employeeclient.data.remote.dto.account.AccountsPageDto
import com.example.employeeclient.data.remote.dto.account.event.AccountEventsPageDto
import retrofit2.http.GET
import retrofit2.http.Path
import retrofit2.http.Query

interface AccountApi {

    @GET("/manage/accounts/{id}")
    suspend fun getAccountById(
        @Path("id") id: String
    ): AccountDto

    @GET("/manage/accounts/of/{id}")
    suspend fun getAllUserAccountsById(
        @Path("id") id: String,
        @Query("Name") name: String? = null,
        @Query("PageNumber") pageNumber: Int = 1,
        @Query("PageSize") pageSize: Int? = 20,
        @Query("SortingType") sortingType: String? = null,
    ): AccountsPageDto

    @GET("manage/accounts/{id}/history")
    suspend fun getAccountOperations(
        @Path("id") id: String,
        @Query("Name") name: String? = null,
        @Query("PageNumber") pageNumber: Int = 1,
        @Query("PageSize") pageSize: Int? = 20,
        @Query("SortingType") sortingType: String? = null,
        @Query("From") from: String? = null,
        @Query("To") to: String? = null,
    ): AccountEventsPageDto

}