package com.example.customerclient.domain.usecases.exchangeRate

import com.example.customerclient.data.repository.ExchangeRateRepository

class GetDollarExchangeRateUseCase(
    private val exchangeRateRepository: ExchangeRateRepository
) {
    suspend operator fun invoke(): String {
        return exchangeRateRepository.getDollarExchangeRate().toString()
    }
}