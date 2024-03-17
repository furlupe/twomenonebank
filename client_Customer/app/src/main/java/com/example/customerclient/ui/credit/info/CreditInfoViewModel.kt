package com.example.customerclient.ui.credit.info

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.credit.GetCreditHistoryUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditInfoUseCase
import com.example.customerclient.domain.usecases.credit.PayCreditUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditInfoViewModel(
    handle: SavedStateHandle,
    private val getCreditInfoUseCase: GetCreditInfoUseCase,
    private val getCreditHistoryUseCase: GetCreditHistoryUseCase,
    private val payCreditUseCase: PayCreditUseCase
) : ViewModel() {

    private val creditId = handle.get<String>("creditId")

    private val _uiState: MutableStateFlow<CreditInfoState> = MutableStateFlow(CreditInfoState())
    val uiState: StateFlow<CreditInfoState> = _uiState.asStateFlow()

    private val _creditsHistoryState: MutableStateFlow<PagingData<CreditHistory>> =
        MutableStateFlow(value = PagingData.empty())
    val creditsHistoryState: MutableStateFlow<PagingData<CreditHistory>> get() = _creditsHistoryState

    init {
        viewModelScope.launch { getCreditInfo() }
        viewModelScope.launch { getCreditsHistory() }
    }

    private suspend fun getCreditInfo() {
        creditId?.let {
            try {
                val creditInfo = getCreditInfoUseCase(creditId)
                _uiState.update {
                    CreditInfoState(info = creditInfo)
                }
            } catch (e: Exception) {
                e.message?.let { }
            }
        }
    }

    private suspend fun getCreditsHistory() {
        creditId?.let {
            try {
                getCreditHistoryUseCase(creditId)
                    .distinctUntilChanged()
                    .cachedIn(viewModelScope)
                    .collect { creditsHistory ->
                        _creditsHistoryState.value = creditsHistory
                    }
            } catch (e: Exception) {
                e.message?.let { }
            }
        }
    }

    fun payOffLoan() {
        viewModelScope.launch {
            try {
                creditId?.let { payCreditUseCase(it) }
                getCreditInfo()
                getCreditsHistory()
            } catch (e: Exception) {

            }
        }
    }
}

data class CreditInfoState(
    val info: CreditInfo = CreditInfo(),
)

data class CreditInfo(
    val id: String = "",
    val date: String = "",
    val amount: String = "",
    val penalty: Int = 0,
)

data class CreditHistory(
    val id: String,
    val type: Int,
    val date: String,
    val to: String?,
    val amount: String
)