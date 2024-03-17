package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class DepositUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(billId: String, amount: Int) =
        billRepository.deposit(billId, amount)
}