package com.example.customerclient.data.repository

import com.example.customerclient.ui.bottombar.home.BillInfo

interface BillRepository {
    fun getUserBillsInfo(userId: String): List<BillInfo>
}