package com.example.employeeclient.domain.usecase.db.tariff

import com.example.employeeclient.domain.model.tariff.TariffDomain
import com.example.employeeclient.domain.repository.db.TariffSyncRepository

class InsertTariffUseCase(
    private  val repository: TariffSyncRepository
) {
    suspend operator fun invoke(tariff: TariffDomain) {
        repository.upsert(tariff)
    }
}