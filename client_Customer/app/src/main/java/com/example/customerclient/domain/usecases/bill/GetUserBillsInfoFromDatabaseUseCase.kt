package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class GetUserBillsInfoFromDatabaseUseCase(
    private val billRepository: BillRepository
) {
    suspend operator fun invoke() = billRepository.getUserBillsInfoFromDatabase()
}