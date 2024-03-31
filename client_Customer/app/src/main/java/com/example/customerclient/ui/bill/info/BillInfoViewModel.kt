package com.example.customerclient.ui.bill.info

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.bill.CloseBillUseCase
import com.example.customerclient.domain.usecases.bill.DepositUseCase
import com.example.customerclient.domain.usecases.bill.GetBillHistoryUseCase
import com.example.customerclient.domain.usecases.bill.GetBillInfoUseCase
import com.example.customerclient.domain.usecases.bill.WithdrawUseCase
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class BillInfoViewModel(
    handle: SavedStateHandle,
    private val getBillInfoUseCase: GetBillInfoUseCase,
    private val getBillHistoryUseCase: GetBillHistoryUseCase,
    private val depositUseCase: DepositUseCase,
    private val withdrawUseCase: WithdrawUseCase,
    private val closeBillUseCase: CloseBillUseCase
) : ViewModel() {

    private val billId = handle.get<String>("billId")

    private val _uiState: MutableStateFlow<BillInfoState> =
        MutableStateFlow(BillInfoState.Loading)
    val uiState: StateFlow<BillInfoState> = _uiState.asStateFlow()

    private val _billsHistoryState: MutableStateFlow<PagingData<BillHistory>> =
        MutableStateFlow(value = PagingData.empty())
    val billsHistoryState: MutableStateFlow<PagingData<BillHistory>> get() = _billsHistoryState

    init {
        viewModelScope.launch { getBillsHistory() }
        viewModelScope.launch { getBillInfo() }
    }

    private suspend fun getBillInfo() {
        billId?.let {
            try {
                val billInfo = getBillInfoUseCase(billId)
                _uiState.update {
                    BillInfoState.Content(info = billInfo)
                }
            } catch (e: Exception) {
                e.message?.let {
                    _uiState.update { BillInfoState.Error(e.message ?: "") }
                }
            }
        }
    }

    private suspend fun getBillsHistory() {
        billId?.let {
            try {
                getBillHistoryUseCase(billId)
                    .distinctUntilChanged()
                    .cachedIn(viewModelScope)
                    .collect {
                        _billsHistoryState.value = it
                    }
            } catch (e: Exception) {
                e.message?.let {
                    _uiState.update { BillInfoState.Error(e.message ?: "") }
                }
            }
        }
    }

    fun topUpBill(amount: String) {
        viewModelScope.launch {
            billId?.let {
                try {
                    depositUseCase(billId, amount.toDouble())
                    getBillInfo()
                    getBillsHistory()
                } catch (e: Exception) {
                    _uiState.update { BillInfoState.Error(e.message ?: "") }
                }
            }
        }
    }

    fun chargeBill(amount: String) {
        viewModelScope.launch {
            billId?.let {
                try {
                    withdrawUseCase(billId, amount.toDouble())
                    getBillInfo()
                    getBillsHistory()
                } catch (e: Exception) {
                    _uiState.update { BillInfoState.Error(e.message ?: "") }
                }
            }
        }
    }

    fun closeBill() {
        viewModelScope.launch {
            billId?.let {
                try {
                    closeBillUseCase(billId)
                    _uiState.update { BillInfoState.NavigateToMainScreen }
                } catch (e: Exception) {
                    _uiState.update { BillInfoState.Error(e.message ?: "") }
                }
            }
        }
    }

}

sealed class BillInfoState {
    data object Loading : BillInfoState()
    data class Content(val info: BillInfo) : BillInfoState()
    data object NavigateToMainScreen : BillInfoState()
    data class Error(val errorMessage: String) : BillInfoState()
}

data class BillHistory(
    val id: String = "",
    val type: OperationType = OperationType.BALANCE_CHANGE,
    val eventType: HistoryOperationType = HistoryOperationType.WITHDRAW,
    val date: String = "",
    val amount: String = "",
    val billId: String = ""
)

enum class HistoryOperationType {
    WITHDRAW, TOP_UP
}

enum class OperationType {
    BALANCE_CHANGE, TRANSFER
}