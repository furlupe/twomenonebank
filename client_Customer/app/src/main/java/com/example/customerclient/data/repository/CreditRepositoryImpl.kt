package com.example.customerclient.data.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import com.example.customerclient.common.Constants.PAGE_CREDIT_LIMIT
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.api.dto.CreateCreditDto
import com.example.customerclient.data.api.dto.toCreditInfo
import com.example.customerclient.data.paging.credit.CreditsHistoryPagingSource
import com.example.customerclient.data.paging.credit.CreditsPagingSource
import com.example.customerclient.data.paging.credit.TariffsPagingSource
import com.example.customerclient.data.remote.database.CreditDao
import com.example.customerclient.data.remote.database.entity.CreditEntity
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.credit.create.Tariff
import com.example.customerclient.ui.credit.info.CreditHistory
import com.example.customerclient.ui.credit.info.CreditInfo
import com.example.customerclient.ui.home.CreditShortInfo
import kotlinx.coroutines.flow.Flow

class CreditRepositoryImpl(
    private val creditsApi: CreditsApi,
    private val creditDao: CreditDao
) : CreditRepository {
    override suspend fun saveUserCreditsToDatabase(credits: List<CreditShortInfo>) {
        val currentCreditsIds = creditDao.getCredits().map { it.id }
        val newBills = credits.filter { it.id !in currentCreditsIds }
        newBills.map {
            creditDao.insertCredit(
                CreditEntity(
                    it.id,
                    it.balance,
                    it.type ?: "",
                    it.date,
                    it.isClosed
                )
            )
        }
    }

    override suspend fun getUserCreditsPagingInfo(): Flow<PagingData<CreditShortInfo>> {
        return Pager(
            config = PagingConfig(pageSize = PAGE_CREDIT_LIMIT),
            pagingSourceFactory = {
                CreditsPagingSource(
                    creditsApi
                )
            }
        ).flow
    }

    override suspend fun getUserCreditsInfoFromDatabase(): List<CreditShortInfo> {
        return creditDao.getCredits().map { entity ->
            CreditShortInfo(
                entity.id,
                entity.tariff,
                entity.amount,
                entity.days,
                "Количество дней",
                entity.isClosed
            )
        }
    }

    override suspend fun getUserCreditsInfo(): List<CreditShortInfo> {
        return creditsApi.getUserCredits(1).items.map { it.toCreditInfo() }
    }

    override suspend fun getCreditInfo(creditId: String): CreditInfo {
        return creditsApi.getCreditInfo(creditId).toCreditInfo()
    }

    override suspend fun getCreditHistory(creditId: String): Flow<PagingData<CreditHistory>> {
        return Pager(
            config = PagingConfig(pageSize = PAGE_CREDIT_LIMIT),
            pagingSourceFactory = {
                CreditsHistoryPagingSource(
                    creditsApi,
                    creditId
                )
            }
        ).flow
    }

    override suspend fun createCredit(
        tariffId: String,
        withdrawalAccountId: String,
        destinationAccountId: String,
        amount: Int,
        days: Int
    ) {
        return creditsApi.createCredit(
            CreateCreditDto(
                tariffId,
                withdrawalAccountId,
                destinationAccountId,
                amount,
                days
            )
        )
    }

    override suspend fun getCreditsTariffs(): Flow<PagingData<Tariff>> {
        return Pager(
            config = PagingConfig(pageSize = PAGE_CREDIT_LIMIT),
            pagingSourceFactory = {
                TariffsPagingSource(creditsApi)
            }
        ).flow
    }

    override suspend fun payCredit(creditId: String) {
        return creditsApi.payCredit(creditId)
    }

    override suspend fun getUserCreditRate(): String {
        return creditsApi.getUserCreditInfo().creditRating?.toString() ?: "Пока неизвестен"
    }
}