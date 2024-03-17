package com.example.employeeclient.domain.usecase.db

import com.example.employeeclient.domain.usecase.db.ban.SyncBansUseCase
import com.example.employeeclient.domain.usecase.db.tariff.SyncTariffsUseCase
import com.example.employeeclient.domain.usecase.db.user.SyncUsersUseCase

class SyncAllDataUseCase(
    private val syncBansUseCase: SyncBansUseCase,
    private val syncTariffsUseCase: SyncTariffsUseCase,
    private val syncUsersUseCase: SyncUsersUseCase,
) {
    suspend operator fun invoke(): Result<Boolean> {
        var errorCount: Int = 0

        val banSyncResult = syncBansUseCase().onFailure { errorCount += 1}
        val tariffSyncResult = syncTariffsUseCase().onFailure { errorCount += 1}
        val usersSyncResult = syncUsersUseCase().onFailure { errorCount += 1}

        if (banSyncResult.isSuccess && tariffSyncResult.isSuccess && usersSyncResult.isSuccess) {
            return Result.success(true)
        } else if (errorCount == 3) {
            return Result.failure(Throwable("Synchronization failed"))
        } else {
            return Result.failure(Throwable("Partially synced, try again"))
        }
    }
}