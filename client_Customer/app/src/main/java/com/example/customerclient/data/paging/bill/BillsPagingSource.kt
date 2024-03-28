package com.example.customerclient.data.paging.bill

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.customerclient.common.Constants.PAGE_BILL_LIMIT
import com.example.customerclient.data.api.core.AccountsApi
import com.example.customerclient.data.api.dto.toBillInfo
import com.example.customerclient.ui.home.BillInfo
import retrofit2.HttpException
import java.io.IOException

class BillsPagingSource(
    private val api: AccountsApi
) : PagingSource<Int, BillInfo>() {
    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, BillInfo> {
        return try {
            val currentPage = params.key ?: 1
            val credits = api.getUserBills(pageNumber = currentPage, pageSize = PAGE_BILL_LIMIT)
            LoadResult.Page(
                data = credits.items?.map { it.toBillInfo() } ?: listOf(),
                prevKey = if (currentPage == 1) null else currentPage - 1,
                nextKey = if (credits.items != null) null else currentPage.plus(1)
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

    override fun getRefreshKey(state: PagingState<Int, BillInfo>): Int? {
        return state.anchorPosition
    }
}