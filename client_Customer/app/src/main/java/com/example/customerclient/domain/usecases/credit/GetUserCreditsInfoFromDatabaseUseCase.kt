package com.example.customerclient.domain.usecases.credit

import com.example.customerclient.domain.repositories.CreditRepository

class GetUserCreditsInfoFromDatabaseUseCase(
    private val creditRepository: CreditRepository
) {
    suspend operator fun invoke() = creditRepository.getUserCreditsInfoFromDatabase()
}