package com.example.customerclient.data.paging.credit

import androidx.paging.PagingSource
import androidx.paging.PagingState
import com.example.customerclient.data.api.credit.CreditsApi
import com.example.customerclient.data.api.dto.toTariff
import com.example.customerclient.ui.credit.create.Tariff
import retrofit2.HttpException
import java.io.IOException

class TariffsPagingSource(
    private val api: CreditsApi,
) : PagingSource<Int, Tariff>() {
    override suspend fun load(params: LoadParams<Int>): LoadResult<Int, Tariff> {
        return try {
            val currentPage = params.key ?: 1
            val creditsHistory = api.getCreditsTariffs(
                page = currentPage
            )
            LoadResult.Page(
                data = creditsHistory.items?.map { it.toTariff() } ?: listOf(),
                prevKey = if (currentPage == 1) null else currentPage - 1,
                nextKey = if (creditsHistory.items.isNullOrEmpty()) null else currentPage.plus(1)
            )
        } catch (exception: IOException) {
            return LoadResult.Error(exception)
        } catch (exception: HttpException) {
            return LoadResult.Error(exception)
        }
    }

    override fun getRefreshKey(state: PagingState<Int, Tariff>): Int? {
        return state.anchorPosition
    }
}