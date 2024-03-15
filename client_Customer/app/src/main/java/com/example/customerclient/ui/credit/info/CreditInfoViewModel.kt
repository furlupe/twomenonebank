package com.example.customerclient.ui.credit.info

import androidx.lifecycle.SavedStateHandle
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.credit.GetCreditInfoUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditInfoViewModel(
    handle: SavedStateHandle,
    private val getCreditInfoUseCase: GetCreditInfoUseCase
) : ViewModel() {

    private val creditId = handle.get<String>("creditId")

    private val _uiState: MutableStateFlow<CreditInfoState> = MutableStateFlow(CreditInfoState())
    val uiState: StateFlow<CreditInfoState> = _uiState.asStateFlow()

    init {
        viewModelScope.launch {
            creditId?.let {
                val creditHistory = getCreditInfoUseCase(creditId)
                _uiState.update {
                    CreditInfoState(history = creditHistory, amount = "39402231")
                }
            }
        }
    }

    fun payOffLoan() {

    }
}

data class CreditInfoState(
    val history: List<CreditHistory> = listOf(),
    val amount: String = "",
)

data class CreditHistory(
    val date: String,
    val amount: String
)