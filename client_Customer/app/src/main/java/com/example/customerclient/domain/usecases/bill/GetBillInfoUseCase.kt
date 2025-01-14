package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class GetBillInfoUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(billId: String) = billRepository.getBillInfo(billId)
}