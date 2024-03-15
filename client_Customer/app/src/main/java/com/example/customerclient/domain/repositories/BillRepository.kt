package com.example.customerclient.domain.repositories

import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bottombar.home.BillInfo

interface BillRepository {
    suspend fun getUserBillsInfo(userId: String): List<BillInfo>

    suspend fun getBillInfo(billId: String): List<BillHistory>
}