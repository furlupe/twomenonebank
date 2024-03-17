package com.example.customerclient.data.paging.credit

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.api.dto.toCreditHistory
import com.example.customerclient.ui.credit.info.CreditHistory
import retrofit2.HttpException
import java.io.IOException

class CreditsHistoryPagingSource(
    private val api: CreditsApi,
    private val creditId: String,
) : PagingSource<Int, CreditHistory>() {
    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, CreditHistory> {
        return try {
            val currentPage = params.key ?: 1
            val creditsHistory = api.getCreditHistory(creditId, page = currentPage)
            LoadResult.Page(
                data = creditsHistory.items?.map { it.toCreditHistory() } ?: listOf(),
                prevKey = if (currentPage == 1) null else currentPage - 1,
                nextKey = if (creditsHistory.items.isNullOrEmpty()) null else currentPage.plus(1)
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

    override fun getRefreshKey(state: PagingState<Int, CreditHistory>): Int? {
        return state.anchorPosition
    }
}