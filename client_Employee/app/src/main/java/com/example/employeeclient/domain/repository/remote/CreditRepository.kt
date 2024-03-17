package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.domain.model.credit.CreditDetailsDomain
import com.example.employeeclient.domain.model.credit.CreditsPageDomain
import com.example.employeeclient.domain.model.credit.operation.CreditOperationsPageDomain

interface CreditRepository {
    suspend fun getAllCredits(userId: String, page: Int): CreditsPageDomain
    suspend fun getCreditDetails(creditId: String): CreditDetailsDomain
    suspend fun getAllCreditOperations(creditId: String, page: Int): CreditOperationsPageDomain
}