package com.example.customerclient.data.websocket

import android.util.Log
import com.example.customerclient.common.Constants.BASE_CORE_URL
import com.example.customerclient.data.api.dto.AccountEventDto
import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import io.reactivex.rxjava3.core.Single

class BillHistoryWebSocket {
    private lateinit var connection: HubConnection

    fun initializeWebSocket(billId: String, token: String, connectionOn: (List<AccountEventDto>) -> Unit) {
        connection = HubConnectionBuilder
            .create("${BASE_CORE_URL}accounts")
            .withAccessTokenProvider(Single.defer { Single.just(token) })
            .build()

        // Add error handling
        connection.onClosed {
            Log.d("WEB SOCKET", "Connection closed: ${it.message}")
        }

        connection.start().blockingAwait() // Wait for the connection to be established

        // Add error handling for subscription invocation
        try {
            connection.invoke("subscribe", billId).blockingAwait()
            connection.on(
                "ReceiveTransactions",
                { data ->
                    connectionOn(data.toList())
                },
                Array<AccountEventDto>::class.java,
            )
        } catch (ex: Exception) {
            Log.d("WEB SOCKET", "Error invoking subscribe method: ${ex.message}")
        }
    }

    fun close() = connection.close()
}