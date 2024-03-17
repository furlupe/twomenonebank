package com.example.customerclient.domain.usecases.credit

import androidx.paging.PagingData
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.credit.info.CreditHistory
import kotlinx.coroutines.flow.Flow

class GetCreditHistoryUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(creditId: String): Flow<PagingData<CreditHistory>> {
        return creditRepository.getCreditHistory(creditId)
    }
}