package com.example.customerclient.domain.repositories

import com.example.customerclient.ui.bottombar.home.CreditInfo

interface CreditRepository {
    suspend fun getUserCreditsInfo(userId: String): List<CreditInfo>
}