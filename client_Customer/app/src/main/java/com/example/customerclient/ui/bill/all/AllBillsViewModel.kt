package com.example.customerclient.ui.bill.all

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.ui.bottombar.home.BillInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class AllBillsViewModel(
    private val getUserBillsInfoUseCase: GetUserBillsInfoUseCase,
) : ViewModel() {

    private val _uiState: MutableStateFlow<AllBillsState> = MutableStateFlow(AllBillsState())
    val uiState: StateFlow<AllBillsState> = _uiState.asStateFlow()

    init {
        viewModelScope.launch {
            val userId = "0"
            val bills = getUserBillsInfoUseCase(userId)
            _uiState.update { it.copy(bills = bills) }
        }
    }
}

data class AllBillsState(
    val bills: List<BillInfo> = listOf()
)