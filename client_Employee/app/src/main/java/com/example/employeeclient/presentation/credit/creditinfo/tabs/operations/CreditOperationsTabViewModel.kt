package com.example.employeeclient.presentation.credit.creditinfo.tabs.operations

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.repository.remote.CreditRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditOperationsTabViewModel(
    private val creditId: String,
    private val creditRepository: CreditRepository
): ViewModel() {
    private val _state = MutableStateFlow(CreditOperationsTabState())
    val state: StateFlow<CreditOperationsTabState> = _state

    init {
        getCreditOperations(1)
    }

    private fun getCreditOperations(page: Int) = viewModelScope.launch {
        try {
            val credits = creditRepository.getAllCreditOperations(creditId, page)

            _state.update { prevState ->
                prevState.copy(
                    currentPage = credits.currentPage,
                    totalPages = credits.totalPages,
                    items = credits.items,
                    isLastPage = page == credits.totalPages
                )
            }
        } catch (exception: Exception) {
            if (exception.message == "HTTP 404 Not Found") {
                _state.update { prevState ->
                    prevState.copy(
                        items = emptyList(),
                        isLastPage = true
                    )
                }
            }
        }
    }
}