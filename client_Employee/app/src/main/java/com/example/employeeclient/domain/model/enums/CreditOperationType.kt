package com.example.employeeclient.domain.model.enums

enum class CreditOperationType(val value: Int, val descr: String) {
    Closed(0, "Closed"),
    PaymentDateMoved(1, "Payment date moved"),
    PaymentMade(2, "Payment made"),
    PaymentMissed(3, "Payment missed"),
    PenaltyAdded(4, "Penalty added"),
    PenaltyPaid(5, "Penalty paid"),
    RateApplied(6, "Rate applied");

    companion object {
        fun fromInt(value: Int) = CreditOperationType.entries.first { it.value == value }
    }
}