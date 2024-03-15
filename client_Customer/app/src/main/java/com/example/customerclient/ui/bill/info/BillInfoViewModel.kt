package com.example.customerclient.ui.bill.info

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.bill.GetBillInfoUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class BillInfoViewModel(
    handle: SavedStateHandle,
    private val getBillInfoUseCase: GetBillInfoUseCase
) : ViewModel() {

    private val billId = handle.get<String>("billId")

    private val _uiState: MutableStateFlow<BillInfoState> = MutableStateFlow(BillInfoState())
    val uiState: StateFlow<BillInfoState> = _uiState.asStateFlow()

    init {
        viewModelScope.launch {
            billId?.let {
                val billHistory = getBillInfoUseCase(billId)
                _uiState.update {
                    BillInfoState(history = billHistory, moneyOnBill = "400000")
                }
            }
        }
    }

    fun topUpBill(amount: String) {

    }

    fun chargeBill(amount: String) {

    }

    fun closeBill() {

    }

}

data class BillInfoState(
    val history: List<BillHistory> = listOf(),
    val moneyOnBill: String = "",
)

data class BillHistory(
    val type: HistoryOperationType,
    val date: String,
    val amount: String,
    val billNumber: String
)

enum class HistoryOperationType {
    WITHDRAW, TOP_UP
}