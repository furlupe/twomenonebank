package com.example.customerclient.ui.credit.create

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.credit.CreateCreditUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditTariffsUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreateCreditViewModel(
    private val createCreditUseCase: CreateCreditUseCase,
    private val getCreditTariffsUseCase: GetCreditTariffsUseCase,
) : ViewModel() {

    private val _uiState: MutableStateFlow<CreateCreditState> =
        MutableStateFlow(CreateCreditState.Loading)
    val uiState: StateFlow<CreateCreditState> = _uiState.asStateFlow()

    private val _tariffsState: MutableStateFlow<PagingData<Tariff>> =
        MutableStateFlow(value = PagingData.empty())
    val tariffsState: MutableStateFlow<PagingData<Tariff>> get() = _tariffsState

    init {
        viewModelScope.launch { getCreditTariffs() }
    }

    private suspend fun getCreditTariffs() {
        try {
            getCreditTariffsUseCase()
                .distinctUntilChanged()
                .cachedIn(viewModelScope)
                .collect { tariffs -> _tariffsState.value = tariffs }
            _uiState.update { CreateCreditState.Content(null) }
        } catch (e: Exception) {
            _uiState.update { CreateCreditState.Error(e.message.toString()) }
        }
    }

    fun onTariffClick(tariffId: String) {
        _uiState.update { CreateCreditState.Content(tariffId) }
    }

    fun createCredit(
        tariffId: String,
        amount: Int,
        days: Int
    ) {
        viewModelScope.launch {
            try {
                createCreditUseCase(tariffId, amount, days)
                _uiState.update { CreateCreditState.NavigateToMainScreen }
            } catch (e: Exception) {
                _uiState.update { CreateCreditState.Error(e.message.toString()) }
            }
        }
    }
}

sealed class CreateCreditState {
    data class Content(val currentTariffId: String?) : CreateCreditState()
    data object Loading : CreateCreditState()
    data object NavigateToMainScreen : CreateCreditState()
    data class Error(val errorMessage: String) : CreateCreditState()
}

data class Tariff(
    val id: String,
    val name: String,
    val rate: String,
)