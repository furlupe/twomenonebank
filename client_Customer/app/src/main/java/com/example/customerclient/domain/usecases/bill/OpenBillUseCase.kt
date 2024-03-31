package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class OpenBillUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(name: String, currency: String) =
        billRepository.openBill(name, currency)
}