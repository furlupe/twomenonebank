package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository
import com.example.customerclient.ui.bottombar.home.CreditShortInfo

class GetUserCreditsInfoUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(): List<CreditShortInfo> = creditRepository.getUserCreditsInfo()
}