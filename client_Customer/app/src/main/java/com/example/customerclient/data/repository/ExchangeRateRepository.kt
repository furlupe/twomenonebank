package com.example.customerclient.data.repository

interface ExchangeRateRepository {
    fun getDollarExchangeRate(): Double
    fun getEuroExchangeRate(): Double
}