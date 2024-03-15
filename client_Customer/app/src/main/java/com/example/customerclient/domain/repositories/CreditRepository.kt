package com.example.customerclient.domain.repositories

import com.example.customerclient.ui.bottombar.home.CreditInfo
import com.example.customerclient.ui.credit.info.CreditHistory

interface CreditRepository {
    suspend fun getUserCreditsInfo(userId: String): List<CreditInfo>

    suspend fun getCreditInfo(creditId: String): List<CreditHistory>
}