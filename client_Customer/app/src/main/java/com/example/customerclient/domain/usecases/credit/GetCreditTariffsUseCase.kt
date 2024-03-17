package com.example.customerclient.domain.usecases.credit

import androidx.paging.PagingData
import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.credit.create.Tariff
import kotlinx.coroutines.flow.Flow

class GetCreditTariffsUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke() : Flow<PagingData<Tariff>> = creditRepository.getCreditsTariffs()
}