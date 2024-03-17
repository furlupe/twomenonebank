package com.example.customerclient.data.api.core

import com.example.customerclient.data.api.dto.DepositDto
import com.example.customerclient.data.api.dto.WithdrawDto
import retrofit2.http.Body
import retrofit2.http.POST
import retrofit2.http.Path

interface TransactionsApi {
    @POST("accounts/{id}/deposit")
    suspend fun deposit(@Path("id") billId: String, @Body depositBody: DepositDto)

    @POST("accounts/{id}/withdraw")
    suspend fun withdraw(@Path("id") billId: String, @Body withdrawBody: WithdrawDto)
}