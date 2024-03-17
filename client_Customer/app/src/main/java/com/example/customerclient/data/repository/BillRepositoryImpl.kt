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
import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bottombar.home.BillInfo
import kotlinx.coroutines.flow.Flow

class BillRepositoryImpl(
    private val accountsApi: AccountsApi,
    private val transactionsApi: TransactionsApi
) : BillRepository {
    override suspend fun getUserBillsInfo(): List<BillInfo> {
        return accountsApi.getUserBills(1, 30).items?.map { it.toBillInfo() } ?: listOf()
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