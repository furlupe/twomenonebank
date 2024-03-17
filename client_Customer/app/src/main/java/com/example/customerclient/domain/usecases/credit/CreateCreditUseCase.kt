package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository

class CreateCreditUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(
        tariffId: String,
        amount: Int,
        days: Int
    ) {
        creditRepository.createCredit(tariffId, amount, days)
    }
}