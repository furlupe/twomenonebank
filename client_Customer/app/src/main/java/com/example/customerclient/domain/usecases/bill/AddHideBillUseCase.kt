package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class AddHideBillUseCase(private val billRepository: BillRepository) {
    suspend operator fun invoke(billId: String) = billRepository.addHideBill(billId)
}