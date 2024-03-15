package com.example.customerclient.data.repository

import com.example.customerclient.billHistory
import com.example.customerclient.billsInfo
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bottombar.home.BillInfo

class BillRepositoryImpl : BillRepository {
    override suspend fun getUserBillsInfo(userId: String): List<BillInfo> {
        return billsInfo
    }

    override suspend fun getBillInfo(billId: String): List<BillHistory> {
        return billHistory
    }
}