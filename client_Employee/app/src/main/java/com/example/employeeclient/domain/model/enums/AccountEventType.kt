package com.example.employeeclient.domain.model.enums

enum class AccountEventType(val descr: String) {
    BalanceChange("Balance change"),
    Transfer("Transfer");

    companion object {
        fun fromString(value: String) = AccountEventType.entries.first { it.name == value }
    }
}