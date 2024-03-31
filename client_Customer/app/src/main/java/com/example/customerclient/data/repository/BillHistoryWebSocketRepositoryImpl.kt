package com.example.customerclient.data.repository

import com.example.customerclient.data.api.dto.AccountEventDto
import com.example.customerclient.data.websocket.BillHistoryWebSocket
import com.example.customerclient.domain.repositories.BillHistoryWebSocketRepository

class BillHistoryWebSocketRepositoryImpl(
    private val billHistoryWebSocket: BillHistoryWebSocket
) : BillHistoryWebSocketRepository {
    override fun initializeWebSocket(billId: String, token: String, onBillHistoryGet: (List<AccountEventDto>) -> Unit) {
        billHistoryWebSocket.initializeWebSocket(billId, token, onBillHistoryGet)
    }

    override fun closeSocket() {
        billHistoryWebSocket.close()
    }
}