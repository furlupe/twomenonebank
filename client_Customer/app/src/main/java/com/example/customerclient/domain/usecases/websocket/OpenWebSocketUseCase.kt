package com.example.customerclient.domain.usecases.websocket

import com.example.customerclient.data.api.dto.AccountEventDto
import com.example.customerclient.domain.repositories.BillHistoryWebSocketRepository
import com.example.customerclient.domain.usecases.auth.GetTokenUseCase

class OpenWebSocketUseCase(
    private val billHistoryWebSocketRepository: BillHistoryWebSocketRepository,
    private val getTokenUseCase: GetTokenUseCase
) {
    operator fun invoke(billId: String, onBillHistoryGet: (List<AccountEventDto>) -> Unit) {
        val token = getTokenUseCase()
        billHistoryWebSocketRepository.initializeWebSocket(billId, token, onBillHistoryGet)
    }
}