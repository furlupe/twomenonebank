package com.example.customerclient.domain.usecases.websocket

import com.example.customerclient.domain.repositories.BillHistoryWebSocketRepository

class CloseWebSocketUseCase(
    private val billHistoryWebSocketRepository: BillHistoryWebSocketRepository
) {
    operator fun invoke() = billHistoryWebSocketRepository.closeSocket()
}