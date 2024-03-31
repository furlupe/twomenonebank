package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository

class GetUserCreditRateUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(): String {
        return creditRepository.getUserCreditRate()
    }
}