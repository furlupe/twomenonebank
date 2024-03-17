package com.example.customerclient.data.api.dto

import com.example.customerclient.ui.bill.info.BillHistory
import com.example.customerclient.ui.bill.info.HistoryOperationType

data class AccountEventDto(
    val id: String,
    val comment: String?,
    val eventType: String,
    val balanceChange: BalanceChangeDto,
    val transfer: TransferDto?,
    val resolvedAt: String,
    val state: String
)

fun AccountEventDto.toBillHistory() = BillHistory(
    id = this.id,
    type = when (this.balanceChange.eventType) {
        "Withdrawal" -> HistoryOperationType.WITHDRAW
        "Deposit" -> HistoryOperationType.TOP_UP
        else -> HistoryOperationType.WITHDRAW
    },
    amount = this.balanceChange.value.toString(),
    date = this.resolvedAt.split("T")[0],
    billId = this.balanceChange.accountId
)
