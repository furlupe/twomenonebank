package com.example.employeeclient.presentation.credit.creditinfo.tabs.details

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.model.credit.CreditDetailsDomain
import com.example.employeeclient.domain.repository.remote.CreditRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditDetailsTabViewModel(
    creditId: String,
    private val creditRepository: CreditRepository,
): ViewModel() {
    private val _state = MutableStateFlow<CreditDetailsDomain?>(null)
    val state: StateFlow<CreditDetailsDomain?> = _state

    init {
        getCreditDetails(creditId)
    }

    private fun getCreditDetails(creditId: String) = viewModelScope.launch {
        val details = creditRepository.getCreditDetails(creditId)

        _state.update { details }
    }
}