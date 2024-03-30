package com.example.customerclient.domain.usecases.transaction

import com.example.customerclient.domain.repositories.TransactionRepository

class Me2MeTransactionUseCase(private val transactionRepository: TransactionRepository) {
    suspend operator fun invoke(
        sourceId: String,
        targetId: String,
        amount: Double,
        currency: String,
        message: String?
    ) {
        transactionRepository.me2meTransaction(
            sourceId,
            targetId,
            amount,
            currency,
            message
        )
    }
}