package com.example.customerclient.domain.repositories

import androidx.paging.PagingData
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.flow.Flow

interface BillRepository {
    suspend fun saveUserBillsToDatabase(bills: List<BillInfo>)
    suspend fun getUserBillsInfo(): List<BillInfo>

    suspend fun getUserBillsInfoFromDatabase(): List<BillInfo>

    suspend fun getUserBillsPagedInfo(): Flow<PagingData<BillInfo>>

    suspend fun getBillInfo(billId: String): BillInfo


    suspend fun getBillHistory(billId: String): Flow<PagingData<BillHistory>>

    suspend fun openBill(name: String)

    suspend fun closeBill(billId: String)

    suspend fun deposit(billId: String, amount: Int)

    suspend fun withdraw(billId: String, amount: Int)
}