package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.bottombar.home.CreditInfo

class GetUserCreditsInfoUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(userId: String): List<CreditInfo> {
        return creditRepository.getUserCreditsInfo(userId)
    }
}