package com.example.employeeclient.domain.model.enums

enum class BalanceChangeType(val descr: String) {
    Withdrawal("Withdrawal"),
    Deposit("Deposit"),
    CreditPayment  ("Credit payment");

    companion object {
        fun getFromString(value: String) = BalanceChangeType.entries.first { it.name == value }
    }
}