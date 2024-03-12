package com.example.customerclient.domain.repositories

import com.example.customerclient.ui.bottombar.home.BillInfo

interface BillRepository {
    fun getUserBillsInfo(userId: String): List<BillInfo>
}