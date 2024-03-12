package com.example.customerclient.domain.repositories

interface ExchangeRateRepository {
    fun getDollarExchangeRate(): Double
    fun getEuroExchangeRate(): Double
}