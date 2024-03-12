package com.example.customerclient.data.repository

import com.example.customerclient.creditsInfo
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.bottombar.home.CreditInfo

class CreditRepositoryImpl : CreditRepository {
    override fun getUserCreditsInfo(userId: String): List<CreditInfo> {
        return creditsInfo
    }
}