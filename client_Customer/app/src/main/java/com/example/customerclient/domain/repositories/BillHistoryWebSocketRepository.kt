package com.example.customerclient.domain.repositories

import com.example.customerclient.data.api.dto.AccountEventDto

interface BillHistoryWebSocketRepository {
    fun initializeWebSocket(billId: String, token: String, onBillHistoryGet: (List<AccountEventDto>) -> Unit)

    fun closeSocket()
}