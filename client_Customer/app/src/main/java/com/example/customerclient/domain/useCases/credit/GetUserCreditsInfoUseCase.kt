package com.example.customerclient.domain.useCases.credit

import com.example.customerclient.data.repository.CreditRepository
import com.example.customerclient.ui.bottombar.home.CreditInfo

class GetUserCreditsInfoUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(userId: String): List<CreditInfo> {
        return creditRepository.getUserCreditsInfo(userId)
    }
}