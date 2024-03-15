package com.example.customerclient.data.repository

import com.example.customerclient.dollarExchangeRate
import com.example.customerclient.domain.repositories.ExchangeRateRepository
import com.example.customerclient.euroExchangeRate

class ExchangeRateRepositoryImpl : ExchangeRateRepository {
    override suspend fun getDollarExchangeRate(): Double {
        return dollarExchangeRate
    }

    override suspend fun getEuroExchangeRate(): Double {
        return euroExchangeRate
    }
}