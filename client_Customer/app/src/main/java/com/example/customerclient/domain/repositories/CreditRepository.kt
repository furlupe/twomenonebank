package com.example.customerclient.domain.repositories

import androidx.paging.PagingData
import com.example.customerclient.ui.bottombar.home.CreditShortInfo
import com.example.customerclient.ui.credit.create.Tariff
import com.example.customerclient.ui.credit.info.CreditHistory
import com.example.customerclient.ui.credit.info.CreditInfo
import kotlinx.coroutines.flow.Flow

interface CreditRepository {
    suspend fun getUserCreditsPagingInfo(): Flow<PagingData<CreditShortInfo>>

    suspend fun getUserCreditsInfo(): List<CreditShortInfo>

    suspend fun getCreditInfo(creditId: String): CreditInfo

    suspend fun getCreditHistory(creditId: String): Flow<PagingData<CreditHistory>>

    suspend fun createCredit(tariffId: String, amount: Int, days: Int)

    suspend fun getCreditsTariffs(): Flow<PagingData<Tariff>>

    suspend fun payCredit(creditId: String)
}