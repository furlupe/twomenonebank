package com.example.employeeclient.domain.model.enums

enum class Currency(val descr: String, val symbol: String) {
    AFN("Afghani", "؋"),
    EUR("Euro", "€"),
    IQD("Dinar", "ع.د"),
    NGN("Naira", "₦"),
    RUB("Rubles", "₽"),
    USD("Dollars", "\$");

    companion object {
        fun fromString(value: String) = entries.firstOrNull { it.name == value } ?: RUB
    }
}