package com.example.customerclient.data.repository

import com.example.customerclient.data.api.dto.DepositDto
import com.example.customerclient.data.api.dto.MoneyDto
import com.example.customerclient.data.api.dto.TransferBodyDto
import com.example.customerclient.data.api.dto.WithdrawDto
import com.example.customerclient.data.api.transactions.TransactionsApi
import com.example.customerclient.domain.repositories.TransactionRepository

class TransactionRepositoryImpl(
    private val transactionsApi: TransactionsApi,
) : TransactionRepository {
    override suspend fun p2pTransaction(
        sourceId: String,
        transfereeIdentifier: String,
        amount: Double,
        currency: String,
        message: String?
    ) {
        val money = MoneyDto(amount, "AFN")
        transactionsApi.p2pTransaction(
            sourceId,
            transfereeIdentifier,
            TransferBodyDto(money, message)
        )
    }

    override suspend fun me2meTransaction(
        sourceId: String,
        targetId: String,
        amount: Double,
        currency: String,
        message: String?
    ) {
        val money = MoneyDto(amount, "AFN")
        transactionsApi.me2meTransaction(
            sourceId,
            targetId,
            TransferBodyDto(money, message)
        )
    }

    override suspend fun deposit(billId: String, amount: Int) {
        transactionsApi.deposit(billId, DepositDto(amount))
    }

    override suspend fun withdraw(billId: String, amount: Int) {
        transactionsApi.withdraw(billId, WithdrawDto(amount))
    }
}