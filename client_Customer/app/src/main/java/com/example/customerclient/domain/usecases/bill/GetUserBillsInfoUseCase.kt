package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository
import com.example.customerclient.ui.bottombar.home.BillInfo

class GetUserBillsInfoUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(userId: String): List<BillInfo> {
        return billRepository.getUserBillsInfo(userId)
    }
}