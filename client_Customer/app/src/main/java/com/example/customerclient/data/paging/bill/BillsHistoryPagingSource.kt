package com.example.customerclient.data.paging.bill

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.customerclient.common.Constants.PAGE_BILL_LIMIT
import com.example.customerclient.data.api.core.AccountsApi
import com.example.customerclient.data.api.dto.toBillHistory
import com.example.customerclient.ui.bill.info.BillHistory
import retrofit2.HttpException
import java.io.IOException

class BillsHistoryPagingSource(
    private val api: AccountsApi,
    private val billId: String,
) : PagingSource<Int, BillHistory>() {
    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, BillHistory> {
        return try {
            val currentPage = params.key ?: 1
            val billsHistory = api.getBillHistory(billId, pageNumber = currentPage, pageSize = PAGE_BILL_LIMIT)
            LoadResult.Page(
                data = billsHistory.items?.map { it.toBillHistory() } ?: listOf(),
                prevKey = if (currentPage == 1) null else currentPage - 1,
                nextKey = if (billsHistory.items.isNullOrEmpty()) null else currentPage.plus(1)
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

    override fun getRefreshKey(state: PagingState<Int, BillHistory>): Int? {
        return state.anchorPosition
    }
}