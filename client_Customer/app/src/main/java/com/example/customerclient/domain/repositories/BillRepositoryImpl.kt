package com.example.customerclient.domain.repositories

import com.example.customerclient.billsInfo
import com.example.customerclient.data.repository.BillRepository
import com.example.customerclient.ui.bottombar.home.BillInfo

class BillRepositoryImpl : BillRepository {
    override fun getUserBillsInfo(userId: String): List<BillInfo> {
        return billsInfo
    }
}