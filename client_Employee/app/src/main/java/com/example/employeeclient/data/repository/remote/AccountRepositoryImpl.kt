package com.example.employeeclient.data.repository.remote

import com.example.employeeclient.data.remote.api.AccountApi
import com.example.employeeclient.data.remote.dto.account.toDomain
import com.example.employeeclient.domain.model.account.AccountDomain
import com.example.employeeclient.domain.model.account.AccountsPageDomain
import com.example.employeeclient.domain.model.account.event.AccountEventsPageDomain
import com.example.employeeclient.domain.repository.remote.AccountRepository

class AccountRepositoryImpl(
    private val api: AccountApi
): AccountRepository {
    override suspend fun getAccount(id: String): AccountDomain {
        return api.getAccountById(id).toDomain()
    }

    override suspend fun getAllUserAccounts(userId: String, page: Int): AccountsPageDomain {
        return api.getAllUserAccountsById(id = userId, pageNumber = page).toDomain()
    }

    override suspend fun getAccountOperations(id: String, page: Int): AccountEventsPageDomain {
        return api.getAccountOperations(id, page).toDomain()
    }

}