package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository

class PayCreditUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke(creditId: String) = creditRepository.payCredit(creditId)
}