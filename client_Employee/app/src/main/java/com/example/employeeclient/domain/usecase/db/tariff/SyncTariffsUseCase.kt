package com.example.employeeclient.domain.usecase.db.tariff

import com.example.employeeclient.domain.model.tariff.TariffCreateDomain
import com.example.employeeclient.domain.repository.db.TariffSyncRepository
import com.example.employeeclient.domain.repository.remote.TariffRepository

class SyncTariffsUseCase(
    private val syncRepository: TariffSyncRepository,
    private val repository: TariffRepository
) {
    suspend operator fun invoke(): Result<Boolean> {
        val dbTariffs = syncRepository.getAll()

        if (dbTariffs.isEmpty()) return Result.success(true)

        var successCount: Int = 0
        dbTariffs.forEach { tariff ->
            try {
                val body = TariffCreateDomain(
                    name = tariff.name,
                    rate = tariff.rate
                )
                repository.createTariff(body)

                syncRepository.delete(tariff)
                successCount += 1
            } catch (ex: Exception) {
                val text = if (successCount == 0)
                    "Sync failed, check connection"
                else
                    "Partially synced, try again"

                return Result.failure(Throwable(text))
            }
        }

        return Result.success(true)
    }
}