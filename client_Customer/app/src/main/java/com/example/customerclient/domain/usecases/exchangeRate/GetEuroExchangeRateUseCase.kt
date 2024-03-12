package com.example.customerclient.domain.usecases.exchangeRate

import com.example.customerclient.domain.repositories.ExchangeRateRepository

class GetEuroExchangeRateUseCase(
    private val exchangeRateRepository: ExchangeRateRepository
) {
    suspend operator fun invoke(): String {
        return exchangeRateRepository.getEuroExchangeRate().toString()
    }
}