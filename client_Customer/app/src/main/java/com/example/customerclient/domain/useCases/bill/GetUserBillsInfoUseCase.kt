package com.example.customerclient.domain.useCases.bill

import com.example.customerclient.data.repository.BillRepository
import com.example.customerclient.ui.bottombar.home.BillInfo

class GetUserBillsInfoUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke(userId: String): List<BillInfo> {
        return billRepository.getUserBillsInfo(userId)
    }
}