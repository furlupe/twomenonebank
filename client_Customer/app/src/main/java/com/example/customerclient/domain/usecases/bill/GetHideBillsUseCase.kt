package com.example.customerclient.domain.usecases.bill

import com.example.customerclient.domain.repositories.BillRepository

class GetHideBillsUseCase(private val billRepository: BillRepository) {
    suspend operator fun invoke() = billRepository.getHideBillsIds()
}