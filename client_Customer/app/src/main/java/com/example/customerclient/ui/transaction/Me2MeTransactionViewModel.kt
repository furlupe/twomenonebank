package com.example.customerclient.ui.transaction

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.bill.GetUserBillsPagedInfoUseCase
import com.example.customerclient.domain.usecases.transaction.Me2MeTransactionUseCase
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class Me2MeTransactionViewModel(
    handle: SavedStateHandle,
    private val me2MeTransactionUseCase: Me2MeTransactionUseCase,
    private val getUserBillsPagedInfoUseCase: GetUserBillsPagedInfoUseCase,
) : ViewModel() {
    private var billId = handle.get<String>("billId")
    private var currency = handle.get<String>("currency")

    private lateinit var targetId: String

    private val _uiState: MutableStateFlow<Me2MeTransactionState> =
        MutableStateFlow(Me2MeTransactionState.ChooseBillContent())
    val uiState: StateFlow<Me2MeTransactionState> = _uiState.asStateFlow()

    private val _billsInfoState: MutableStateFlow<PagingData<BillInfo>> =
        MutableStateFlow(value = PagingData.empty())
    val billsInfoState: MutableStateFlow<PagingData<BillInfo>> get() = _billsInfoState

    fun chooseBill(billId: String) {
        targetId = billId
        _uiState.update { Me2MeTransactionState.TransactionContent }
    }

    fun me2MeTransaction(amount: String, message: String) {
        viewModelScope.launch {
            try {
                billId?.let {
                    currency?.let { currency ->
                        me2MeTransactionUseCase(
                            it,
                            targetId,
                            amount.toDouble(),
                            currency,
                            message
                        )
                    }
                }
            } catch (e: Throwable) {}
        }
    }

    fun getBills() {
        viewModelScope.launch {
            try {
                getUserBillsPagedInfoUseCase()
                    .distinctUntilChanged()
                    .cachedIn(viewModelScope)
                    .collect { bills: PagingData<BillInfo> ->
                        _billsInfoState.value = bills
                    }
            } catch (e: Exception) { }

        }
    }
}

sealed class Me2MeTransactionState {
    data class ChooseBillContent(
        val bills: List<BillInfo> = listOf(),
    ) : Me2MeTransactionState()

    data object TransactionContent : Me2MeTransactionState()

    data class Error(val errorMessage: String) : Me2MeTransactionState()

}