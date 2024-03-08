package com.example.customerclient.domain.useCases.exchangeRate

import com.example.customerclient.data.repository.ExchangeRateRepository

class GetEuroExchangeRateUseCase(
    private val exchangeRateRepository: ExchangeRateRepository
) {
    suspend operator fun invoke(): String {
        return exchangeRateRepository.getEuroExchangeRate().toString()
    }
}