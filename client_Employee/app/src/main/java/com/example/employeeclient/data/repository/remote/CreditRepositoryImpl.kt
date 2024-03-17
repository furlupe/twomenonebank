package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.CreditApi
import com.example.employeeclient.data.remote.dto.credit.toDomain
import com.example.employeeclient.domain.model.credit.CreditDetailsDomain
import com.example.employeeclient.domain.model.credit.CreditsPageDomain
import com.example.employeeclient.domain.model.credit.operation.CreditOperationsPageDomain
import com.example.employeeclient.domain.repository.remote.CreditRepository

class CreditRepositoryImpl(
    private val api: CreditApi
): CreditRepository {
    override suspend fun getAllCredits(userId: String, page: Int): CreditsPageDomain {
        return api.getAllUserCredits(userId, page).toDomain()
    }

    override suspend fun getCreditDetails(creditId: String): CreditDetailsDomain {
        return api.getCreditDetails(creditId).toDomain()
    }

    override suspend fun getAllCreditOperations(creditId: String, page: Int): CreditOperationsPageDomain {
        return api.getAllCreditOperations(creditId, page).toDomain()
    }
}