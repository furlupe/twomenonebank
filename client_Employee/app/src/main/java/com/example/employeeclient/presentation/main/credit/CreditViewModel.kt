package com.example.employeeclient.presentation.main.credit

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.model.tariff.TariffCreateDomain
import com.example.employeeclient.domain.model.tariff.TariffDomain
import com.example.employeeclient.domain.repository.remote.TariffRepository
import com.example.employeeclient.domain.usecase.db.tariff.InsertTariffUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditViewModel(
    private val insertTariffUseCase: InsertTariffUseCase,
    private val tariffRepository: TariffRepository
) : ViewModel() {
    private val _state = MutableStateFlow(CreateTariffState())
    var state: StateFlow<CreateTariffState> = _state

    fun create(
        name: String,
        rate: Int
    ) = viewModelScope.launch {
        val body = TariffCreateDomain(
            name, rate
        )

        _state.update {
            it.copy(isLoading = true, created = false, error = "")
        }

        try {
            tariffRepository.createTariff(body)

            _state.update { it.copy(isLoading = false, created = true, error = "") }
        } catch (ex: Exception) {
            insertTariffUseCase(TariffDomain("0", name, rate))

            _state.update {
                it.copy(
                    isLoading = false,
                    created = false,
                    error = ex.message ?: "Registration error"
                )
            }
        }
    }
}