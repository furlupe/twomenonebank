package com.example.customerclient.domain.usecases.transaction

import com.example.customerclient.domain.repositories.TransactionRepository

class P2PTransactionUseCase(private val transactionRepository: TransactionRepository) {
    suspend operator fun invoke(
        sourceId: String,
        transfereeIdentifier: String,
        amount: Double,
        currency: String,
        message: String?
    ) {
        transactionRepository.p2pTransaction(
            sourceId,
            transfereeIdentifier,
            amount,
            currency,
            message
        )
    }
}