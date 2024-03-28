package com.example.customerclient.data.repository

import androidx.paging.Pager
import androidx.paging.PagingConfig
import androidx.paging.PagingData
import com.example.customerclient.common.Constants
import com.example.customerclient.data.api.core.AccountsApi
import com.example.customerclient.data.api.core.TransactionsApi
import com.example.customerclient.data.api.dto.AccountCreateDto
import com.example.customerclient.data.api.dto.DepositDto
import com.example.customerclient.data.api.dto.WithdrawDto
import com.example.customerclient.data.api.dto.toBillInfo
import com.example.customerclient.data.paging.bill.BillsHistoryPagingSource
import com.example.customerclient.data.paging.bill.BillsPagingSource
import com.example.customerclient.data.remote.database.BillDao
import com.example.customerclient.data.remote.database.entity.BillEntity
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.flow.Flow

class BillRepositoryImpl(
    private val accountsApi: AccountsApi,
    private val transactionsApi: TransactionsApi,
    private val billDao: BillDao
) : BillRepository {
    override suspend fun saveUserBillsToDatabase(bills: List<BillInfo>) {
        val currentBillsIds = billDao.getBills().map { it.id }
        val newBills = bills.filter { it.id !in currentBillsIds }
        newBills.map {
            billDao.insertBill(
                BillEntity(
                    it.id,
                    it.name,
                    it.balance,
                    it.type,
                    it.duration
                )
            )
        }
    }

    override suspend fun getUserBillsInfo(): List<BillInfo> {
        return accountsApi.getUserBills(1, 30).items?.map { it.toBillInfo() } ?: listOf()
    }

    override suspend fun getUserBillsInfoFromDatabase(): List<BillInfo> {
        return billDao.getBills().map { BillInfo(it.id, it.name, it.balance, it.type, it.duration) }
    }

    override suspend fun getUserBillsPagedInfo(): Flow<PagingData<BillInfo>> {
        return Pager(
            config = PagingConfig(pageSize = Constants.PAGE_BILL_LIMIT),
            pagingSourceFactory = { BillsPagingSource(accountsApi) }
        ).flow
    }

    override suspend fun getBillInfo(billId: String): BillInfo {
        return accountsApi.getBillInfo(billId).toBillInfo()
    }

    override suspend fun getBillHistory(billId: String): Flow<PagingData<BillHistory>> {
        return Pager(
            config = PagingConfig(pageSize = Constants.PAGE_BILL_LIMIT),
            pagingSourceFactory = { BillsHistoryPagingSource(accountsApi, billId) }
        ).flow
    }

    override suspend fun openBill(name: String) {
        accountsApi.openBill(AccountCreateDto(name))
    }

    override suspend fun closeBill(billId: String) {
        accountsApi.closeBill(billId)
    }

    override suspend fun deposit(billId: String, amount: Int) {
        transactionsApi.deposit(billId, DepositDto(amount))
    }

    override suspend fun withdraw(billId: String, amount: Int) {
        transactionsApi.withdraw(billId, WithdrawDto(amount))
    }


}