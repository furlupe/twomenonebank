package com.example.customerclient.domain.repositories

import androidx.paging.PagingData
import com.example.customerclient.ui.credit.create.Tariff
import com.example.customerclient.ui.credit.info.CreditHistory
import com.example.customerclient.ui.credit.info.CreditInfo
import com.example.customerclient.ui.home.CreditShortInfo
import kotlinx.coroutines.flow.Flow

interface CreditRepository {
    suspend fun saveUserCreditsToDatabase(credits: List<CreditShortInfo>)
    suspend fun getUserCreditsPagingInfo(): Flow<PagingData<CreditShortInfo>>

    suspend fun getUserCreditsInfoFromDatabase(): List<CreditShortInfo>

    suspend fun getUserCreditsInfo(): List<CreditShortInfo>

    suspend fun getCreditInfo(creditId: String): CreditInfo

    suspend fun getCreditHistory(creditId: String): Flow<PagingData<CreditHistory>>

    suspend fun createCredit(
        tariffId: String,
        withdrawalAccountId: String,
        destinationAccountId: String,
        amount: Int,
        days: Int
    )

    suspend fun getCreditsTariffs(): Flow<PagingData<Tariff>>

    suspend fun payCredit(creditId: String)

    suspend fun getUserCreditRate(): String
}