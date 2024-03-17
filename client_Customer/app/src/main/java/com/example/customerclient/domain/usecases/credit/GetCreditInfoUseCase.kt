package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.credit.info.CreditInfo

class GetCreditInfoUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(creditId: String): CreditInfo {
        return creditRepository.getCreditInfo(creditId)
    }
}