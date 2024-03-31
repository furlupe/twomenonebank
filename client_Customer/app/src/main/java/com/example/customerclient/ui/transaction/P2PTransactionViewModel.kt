package com.example.customerclient.ui.transaction

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.transaction.P2PTransactionUseCase
import kotlinx.coroutines.launch

class P2PTransactionViewModel(
    handle: SavedStateHandle,
    private val p2pTransactionUseCase: P2PTransactionUseCase,
) : ViewModel() {
    private val billId = handle.get<String>("billId")
    private val currency = handle.get<String>("currency")

    fun p2pTransaction(
        phone: String,
        amount: String,
        message: String?
    ) {
        viewModelScope.launch {
            try {
                billId?.let {
                    if (currency != null) {
                        p2pTransactionUseCase(
                            billId,
                            phone,
                            amount.toDouble(),
                            currency,
                            message
                        )
                    }
                }
            } catch (e: Throwable) {
            }
        }
    }


}