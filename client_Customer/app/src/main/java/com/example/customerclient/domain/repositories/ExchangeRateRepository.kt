package com.example.customerclient.domain.repositories

interface ExchangeRateRepository {
    suspend fun getDollarExchangeRate(): Double
    suspend fun getEuroExchangeRate(): Double
}