package com.example.employeeclient.domain.usecase.db.user

import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.repository.db.UserSyncRepository
import com.example.employeeclient.domain.repository.remote.UserRepository

class SyncUsersUseCase(
    private val syncRepository: UserSyncRepository,
    private val repository: UserRepository
) {
    suspend operator fun invoke(): Result<Boolean> {
        val dbUsers = syncRepository.getAll()

        if (dbUsers.isEmpty()) return Result.success(true)

        var successCount: Int = 0
        dbUsers.forEach { user ->
            try {
                val body = RegisterInfoDomain(
                    username = user.username,
                    email = user.email,
                    password = user.password,
                    phone = user.phone,
                    roles = user.roles
                )
                repository.register(body)

                syncRepository.delete(user)
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