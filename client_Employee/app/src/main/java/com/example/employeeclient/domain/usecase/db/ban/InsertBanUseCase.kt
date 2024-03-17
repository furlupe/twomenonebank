package com.example.employeeclient.domain.usecase.db.ban

import com.example.employeeclient.domain.model.db.BanDomain
import com.example.employeeclient.domain.repository.db.BanSyncRepository

class InsertBanUseCase(
    private  val repository: BanSyncRepository
) {
    suspend operator fun invoke(ban: BanDomain) {
        repository.upsert(ban)
    }
}