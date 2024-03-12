package com.example.customerclient.data.repository

import com.example.customerclient.dollarExchangeRate
import com.example.customerclient.domain.repositories.ExchangeRateRepository
import com.example.customerclient.euroExchangeRate

class ExchangeRateRepositoryImpl : ExchangeRateRepository {
    override fun getDollarExchangeRate(): Double {
        return dollarExchangeRate
    }

    override fun getEuroExchangeRate(): Double {
        return euroExchangeRate
    }
}