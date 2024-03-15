package com.example.customerclient.ui.credit.all

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.ui.bottombar.home.CreditInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class AllCreditsViewModel(
    private val getUserCreditsInfoUseCase: GetUserCreditsInfoUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<AllCreditsState> = MutableStateFlow(AllCreditsState())
    val uiState: StateFlow<AllCreditsState> = _uiState.asStateFlow()

    init {
        viewModelScope.launch {
            val userId = "0"
            val credits = getUserCreditsInfoUseCase(userId)
            _uiState.update { it.copy(credits = credits) }
        }
    }
}

data class AllCreditsState(
    val credits: List<CreditInfo> = listOf()
)