package com.example.customerclient.data.api.transactions

import com.example.customerclient.data.api.dto.CreditPaymentDto
import com.example.customerclient.data.api.dto.DepositDto
import com.example.customerclient.data.api.dto.TransferBodyDto
import com.example.customerclient.data.api.dto.WithdrawDto
import retrofit2.http.Body
import retrofit2.http.POST
import retrofit2.http.Path

interface TransactionsApi {

    @POST("accounts/{sourceId}/transfer/p2p/{transfereeIdentifier}")
    suspend fun p2pTransaction(
        @Path("sourceId ") sourceId: String,
        @Path("transfereeIdentifier") transfereeIdentifier: String,
        @Body transferBody: TransferBodyDto
    )

    @POST("accounts/{sourceId}/transfer/me2me/{targetId}")
    suspend fun me2meTransaction(
        @Path("sourceId") sourceId: String,
        @Path("targetId") targetId: String,
        @Body transferBody: TransferBodyDto
    )

    @POST("accounts/{id}/deposit")
    suspend fun deposit(@Path("id") billId: String, @Body depositBody: DepositDto)

    @POST("accounts/{id}/withdraw")
    suspend fun withdraw(@Path("id") billId: String, @Body withdrawBody: WithdrawDto)

    @POST("accounts/{id}/repay")
    suspend fun repay(@Path("id") billId: String, @Body creditPayment: CreditPaymentDto)
}