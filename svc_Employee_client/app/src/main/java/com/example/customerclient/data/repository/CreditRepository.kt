package com.example.customerclient.data.repository

import com.example.customerclient.ui.bottombar.home.CreditInfo

interface CreditRepository {
    fun getUserCreditsInfo(userId: String): List<CreditInfo>
}