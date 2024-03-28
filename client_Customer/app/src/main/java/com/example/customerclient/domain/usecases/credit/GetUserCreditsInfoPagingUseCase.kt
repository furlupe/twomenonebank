package com.example.customerclient.domain.usecases.credit

import androidx.paging.PagingData
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.home.CreditShortInfo
import kotlinx.coroutines.flow.Flow

class GetUserCreditsInfoPagingUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(): Flow<PagingData<CreditShortInfo>> =
        creditRepository.getUserCreditsPagingInfo()
}