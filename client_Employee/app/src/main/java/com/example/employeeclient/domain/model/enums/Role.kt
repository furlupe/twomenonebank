package com.example.employeeclient.domain.model.enums

enum class Role(val value: Int) {
    Admin(0),
    Employee(1),
    User(2);

    companion object {
        fun fromInt(value: Int) = entries.firstOrNull() { it.value == value } ?: User
    }
}