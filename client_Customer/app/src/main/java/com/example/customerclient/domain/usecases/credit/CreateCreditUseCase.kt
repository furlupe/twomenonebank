package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository

class CreateCreditUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(
        tariffId: String,
        withdrawalAccountId: String,
        destinationAccountId: String,
        amount: Int,
        days: Int
    ) {
        creditRepository.createCredit(
            tariffId,
            withdrawalAccountId,
            destinationAccountId,
            amount,
            days
        )
    }
}