package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class WithdrawUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(billId: String, amount: Int) =
        billRepository.withdraw(billId, amount)
}