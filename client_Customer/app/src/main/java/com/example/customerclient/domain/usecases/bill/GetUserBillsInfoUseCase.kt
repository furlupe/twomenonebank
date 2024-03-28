package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.home.BillInfo

class GetUserBillsInfoUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(): List<BillInfo> {
        return billRepository.getUserBillsInfo()
    }
}