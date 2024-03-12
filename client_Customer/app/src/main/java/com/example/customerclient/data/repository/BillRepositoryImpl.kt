package com.example.customerclient.data.repository

import com.example.customerclient.billsInfo
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bottombar.home.BillInfo

class BillRepositoryImpl : BillRepository {
    override fun getUserBillsInfo(userId: String): List<BillInfo> {
        return billsInfo
    }
}