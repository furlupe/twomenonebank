package com.example.employeeclient.domain.usecase.db.user

import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.repository.db.UserSyncRepository

class InsertUserUseCase(
    private  val repository: UserSyncRepository
) {
    suspend operator fun invoke(user: RegisterInfoDomain) {
        repository.upsert(user)
    }
}