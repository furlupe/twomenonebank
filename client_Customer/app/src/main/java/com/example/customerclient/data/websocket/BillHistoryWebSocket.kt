package com.example.customerclient.data.websocket

import com.example.customerclient.common.Constants.BASE_CORE_URL
import com.example.customerclient.data.api.dto.AccountEventDto
import com.microsoft.signalr.HubConnection
import com.microsoft.signalr.HubConnectionBuilder
import io.reactivex.rxjava3.core.Single
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch

class BillHistoryWebSocket {
    private lateinit var connection: HubConnection

    fun initializeWebSocket(billId: String, token: String, connectionOn: (List<AccountEventDto>) -> Unit) {
        connection = HubConnectionBuilder
            .create("${BASE_CORE_URL}accounts")
            .withAccessTokenProvider(Single.defer { Single.just(token) })
            .build()

        val scope = CoroutineScope(Dispatchers.IO)
        scope.launch {
            while (true) {
                delay(5000L)
            }
        }
        connection.start()
        connection.on(
            "ReceiveTransactions",
            { data ->

            //connectionOn(data.toList())
            },
            Object::class.java,
        )
        connection.invoke("subscribe", billId)
    }

    fun close() = connection.close()
}