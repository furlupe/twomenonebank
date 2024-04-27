package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bill.info.HistoryOperationType
import com.example.customerclient.ui.bill.info.OperationType

data class AccountEventDto(
    val id: String,
    val comment: String?,
    val eventType: String,
    val balanceChange: BalanceChangeDto?,
    val transfer: TransferDto?,
    val resolvedAt: String,
    val state: String
)

fun AccountEventDto.toBillHistory(): BillHistory {
    return when (this.eventType) {
        "Transfer" -> {
            BillHistory(
                id = this.id,
                type = OperationType.TRANSFER,
                eventType = when (this.transfer?.target?.eventType) {
                    "Withdrawal" -> HistoryOperationType.WITHDRAW
                    "Deposit" -> HistoryOperationType.TOP_UP
                    "0" -> HistoryOperationType.WITHDRAW
                    "1" -> HistoryOperationType.TOP_UP
                    else -> HistoryOperationType.WITHDRAW
                },
                amount = "${this.transfer?.target?.nativeValue?.amount} ${this.transfer?.target?.nativeValue?.currency}",
                date = this.resolvedAt.split("T")[0],
                billId = this.balanceChange?.accountId ?: ""
            )
        }

        "BalanceChange" -> {
            BillHistory(
                id = this.id,
                type = OperationType.BALANCE_CHANGE,
                eventType = when (this.balanceChange?.eventType) {
                    "Withdrawal" -> HistoryOperationType.WITHDRAW
                    "Deposit" -> HistoryOperationType.TOP_UP
                    "0" -> HistoryOperationType.WITHDRAW
                    "1" -> HistoryOperationType.TOP_UP
                    else -> HistoryOperationType.WITHDRAW
                },
                amount = "${this.balanceChange?.nativeValue?.amount} ${this.balanceChange?.nativeValue?.currency}",
                date = this.resolvedAt.split("T")[0],
                billId = this.balanceChange?.accountId ?: ""
            )
        }

        else -> {
            BillHistory(
                id = this.id,
                eventType = when (this.balanceChange?.eventType) {
                    "Withdrawal" -> HistoryOperationType.WITHDRAW
                    "Deposit" -> HistoryOperationType.TOP_UP
                    "0" -> HistoryOperationType.WITHDRAW
                    "1" -> HistoryOperationType.TOP_UP
                    else -> HistoryOperationType.WITHDRAW
                },
                amount = "${this.balanceChange?.nativeValue?.amount} ${this.balanceChange?.nativeValue?.currency}",
                date = this.resolvedAt.split("T")[0],
                billId = this.balanceChange?.accountId ?: ""
            )
        }
    }
}
