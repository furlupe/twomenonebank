package com.example.customerclient.data.repository

import com.example.customerclient.creditsInfo
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.bottombar.home.CreditInfo
import com.example.customerclient.ui.credit.info.CreditHistory

class CreditRepositoryImpl : CreditRepository {
    override suspend fun getUserCreditsInfo(userId: String): List<CreditInfo> {
        return creditsInfo
    }

    override suspend fun getCreditInfo(creditId: String): List<CreditHistory> {
        return listOf()
    }
}