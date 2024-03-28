package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.home.BillInfo

class SaveUserBillInfoToDatabaseUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(bills: List<BillInfo>) = billRepository.saveUserBillsToDatabase(bills)
}