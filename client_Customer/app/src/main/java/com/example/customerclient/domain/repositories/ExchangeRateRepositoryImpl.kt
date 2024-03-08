package com.example.customerclient.domain.repositories

import com.example.customerclient.data.repository.ExchangeRateRepository
import com.example.customerclient.dollarExchangeRate
import com.example.customerclient.euroExchangeRate

class ExchangeRateRepositoryImpl : ExchangeRateRepository {
    override fun getDollarExchangeRate(): Double {
        return dollarExchangeRate
    }

    override fun getEuroExchangeRate(): Double {
        return euroExchangeRate
    }
}