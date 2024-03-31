package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.TransactionRepository

class DepositUseCase(
    private val transactionRepository: TransactionRepository
) {
    suspend operator fun invoke(billId: String, amount: Double) =
        transactionRepository.deposit(billId, amount)
}