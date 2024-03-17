package com.example.employeeclient.presentation.main.sync

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.usecase.db.SyncAllDataUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class SyncViewModel(
    private val syncAllDataUseCase: SyncAllDataUseCase
) : ViewModel() {
    private val _state = MutableStateFlow(SyncState())
    val state: StateFlow<SyncState> = _state

    fun sync() = viewModelScope.launch {
        _state.update { prevState ->
            prevState.copy(
                isLoading = true
            )
        }

        syncAllDataUseCase()
            .onSuccess {
                _state.update { prevState ->
                    prevState.copy(
                        isLoading = false,
                        syncResult = true
                    )
                }
            }
            .onFailure {
                _state.update { prevState ->
                    prevState.copy(
                        isLoading = false,
                        syncResult = true,
                        error = it.message ?: "Error occured"
                    )
                }
            }
    }
}