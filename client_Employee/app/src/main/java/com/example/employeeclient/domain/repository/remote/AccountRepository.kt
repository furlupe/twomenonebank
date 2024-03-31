package com.example.employeeclient.domain.repository.remote

import com.example.employeeclient.data.remote.dto.account.event.RequestAccountHistoryBodyDto
import com.example.employeeclient.domain.model.account.AccountDomain
import com.example.employeeclient.domain.model.account.AccountsPageDomain
import com.example.employeeclient.domain.model.account.event.AccountEventsPageDomain

interface AccountRepository {
    suspend fun getAccount(id: String): AccountDomain
    suspend fun getAllUserAccounts(userId: String, page: Int): AccountsPageDomain
    suspend fun getAccountOperations(id: String, info: RequestAccountHistoryBodyDto): AccountEventsPageDomain
}