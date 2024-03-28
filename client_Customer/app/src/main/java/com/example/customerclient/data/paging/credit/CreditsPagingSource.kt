package com.example.customerclient.data.paging.credit

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.api.dto.toCreditInfo
import com.example.customerclient.ui.home.CreditShortInfo
import retrofit2.HttpException
import java.io.IOException

class CreditsPagingSource(
    private val api: CreditsApi
) : PagingSource<Int, CreditShortInfo>() {
    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, CreditShortInfo> {
        return try {
            val currentPage = params.key ?: 1
            val credits = api.getUserCredits(page = currentPage)
            LoadResult.Page(
                data = credits.items.map { it.toCreditInfo() },
                prevKey = if (currentPage == 1) null else currentPage - 1,
                nextKey = if (credits.items.isEmpty()) null else currentPage.plus(1)
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

    override fun getRefreshKey(state: PagingState<Int, CreditShortInfo>): Int? {
        return state.anchorPosition
    }
}