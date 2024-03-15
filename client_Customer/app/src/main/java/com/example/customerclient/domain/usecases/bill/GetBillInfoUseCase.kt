package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bill.info.BillHistory

class GetBillInfoUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(billId: String): List<BillHistory> {
        return billRepository.getBillInfo(billId)
    }
}