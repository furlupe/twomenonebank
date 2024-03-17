package com.example.customerclient.ui.credit.all

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import androidx.paging.filter
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoPagingUseCase
import com.example.customerclient.ui.bottombar.home.CreditShortInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.launch

class AllCreditsViewModel(
    private val getUserCreditsInfoPagingUseCase: GetUserCreditsInfoPagingUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<AllCreditsState> = MutableStateFlow(AllCreditsState())
    val uiState: StateFlow<AllCreditsState> = _uiState.asStateFlow()

    private val _creditShortInfoState: MutableStateFlow<PagingData<CreditShortInfo>> =
        MutableStateFlow(value = PagingData.empty())
    val creditShortInfoState: MutableStateFlow<PagingData<CreditShortInfo>> get() = _creditShortInfoState

    fun getCreditShortInfo() {
        viewModelScope.launch {
            try {
                getUserCreditsInfoPagingUseCase()
                    .distinctUntilChanged()
                    .cachedIn(viewModelScope)
                    .collect { credits ->
                        _creditShortInfoState.value = credits.filter { !it.isClosed }
                    }
            } catch (e: Exception) {
                e.message?.let { }
            }
        }
    }
}

data class AllCreditsState(
    val credits: List<CreditShortInfo> = listOf()
)