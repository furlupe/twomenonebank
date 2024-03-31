package com.example.customerclient.domain.repositories

interface TransactionRepository {
    suspend fun p2pTransaction(
        sourceId: String,
        transfereeIdentifier: String,
        amount: Double,
        currency: String,
        message: String?
    )

    suspend fun me2meTransaction(
        sourceId: String,
        targetId: String,
        amount: Double,
        currency: String,
        message: String?
    )

    suspend fun deposit(billId: String, amount: Double)

    suspend fun withdraw(billId: String, amount: Double)
}