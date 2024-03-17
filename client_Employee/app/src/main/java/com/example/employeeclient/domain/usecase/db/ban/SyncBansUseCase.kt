package com.example.employeeclient.domain.usecase.db.ban

import com.example.employeeclient.domain.repository.db.BanSyncRepository
import com.example.employeeclient.domain.repository.remote.UserRepository

class SyncBansUseCase(
    private val syncRepository: BanSyncRepository,
    private val repository: UserRepository
) {
    suspend operator fun invoke(): Result<Boolean> {
        val dbBans = syncRepository.getAll()

        if (dbBans.isEmpty()) return Result.success(true)

        var successCount: Int = 0
        dbBans.forEach { ban ->
            try {
                val user = repository.getUser(ban.userId)

                if (user.isBanned == ban.isBanned) return@forEach

                if (ban.isBanned) {
                    repository.banUserById(ban.userId)
                } else {
                    repository.unbanUserById(ban.userId)
                }

                syncRepository.delete(ban)
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