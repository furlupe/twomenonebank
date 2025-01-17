package com.example.customerclient.ui.credit.create

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.bill.GetUserBillsPagedInfoUseCase
import com.example.customerclient.domain.usecases.credit.CreateCreditUseCase
import com.example.customerclient.domain.usecases.credit.GetCreditTariffsUseCase
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreateCreditViewModel(
    private val createCreditUseCase: CreateCreditUseCase,
    private val getCreditTariffsUseCase: GetCreditTariffsUseCase,
    private val getUserBillsPagedInfoUseCase: GetUserBillsPagedInfoUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<CreateCreditState> =
        MutableStateFlow(CreateCreditState.Loading)
    val uiState: StateFlow<CreateCreditState> = _uiState.asStateFlow()

    private val _tariffsState: MutableStateFlow<PagingData<Tariff>> =
        MutableStateFlow(value = PagingData.empty())
    val tariffsState: MutableStateFlow<PagingData<Tariff>> get() = _tariffsState

    private val _billsState: MutableStateFlow<PagingData<BillInfo>> =
        MutableStateFlow(value = PagingData.empty())
    val billsState: MutableStateFlow<PagingData<BillInfo>> get() = _billsState

    private var _tariffId: String? = null
    private var _withdrawalAccountId: String? = null
    private var _destinationAccountId: String? = null


    init {
        viewModelScope.launch { getCreditTariffs() }
        viewModelScope.launch { getBills() }
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

    private suspend fun getBills() {
        try {
            getUserBillsPagedInfoUseCase()
                .distinctUntilChanged()
                .cachedIn(viewModelScope)
                .collect { bills -> _billsState.value = bills }
            _uiState.update { CreateCreditState.Content(null) }
        } catch (e: Exception) {
            _uiState.update { CreateCreditState.Error(e.message.toString()) }
        }
    }

    fun onTariffClick(tariffId: String) {
        _tariffId = tariffId
        _uiState.update { CreateCreditState.Content(tariffId) }
    }

    fun onAccountClick(accountId: String) {
        if (_withdrawalAccountId == null) {
            _withdrawalAccountId = accountId
            _uiState.update { CreateCreditState.Content(_tariffId, accountId) }
        } else {
            _destinationAccountId = accountId
            _uiState.update {
                CreateCreditState.Content(
                    _tariffId,
                    _withdrawalAccountId,
                    accountId
                )
            }
        }
    }

    fun createCredit(
        tariffId: String,
        withdrawalAccountId: String,
        destinationAccountId: String,
        amount: Int,
        days: Int
    ) {
        viewModelScope.launch {
            try {
                createCreditUseCase(
                    tariffId,
                    withdrawalAccountId,
                    destinationAccountId,
                    amount,
                    days
                )
                _uiState.update { CreateCreditState.NavigateToMainScreen }
            } catch (e: Exception) {
                _uiState.update { CreateCreditState.Error(e.message.toString()) }
            }
        }
    }
}

sealed class CreateCreditState {
    data class Content(
        val currentTariffId: String? = null,
        val withdrawalAccountId: String? = null,
        val destinationAccountId: String? = null
    ) : CreateCreditState()

    data object Loading : CreateCreditState()
    data object NavigateToMainScreen : CreateCreditState()
    data class Error(val errorMessage: String) : CreateCreditState()
}

data class Tariff(
    val id: String,
    val name: String,
    val rate: String,
)