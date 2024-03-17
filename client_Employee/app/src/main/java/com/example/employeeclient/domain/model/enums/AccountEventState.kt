package com.example.employeeclient.domain.model.enums

enum class AccountEventState(val descr: String) {
    Completed("Completed"),
    Canceled("Canceled"),
    Failed("Failed")
}