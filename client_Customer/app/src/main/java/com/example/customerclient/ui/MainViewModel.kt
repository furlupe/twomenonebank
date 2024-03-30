package com.example.customerclient.ui

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.auth.SignInUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class MainViewModel(private val signInUseCase: SignInUseCase) : ViewModel() {

    private val _uiState: MutableStateFlow<MainState> = MutableStateFlow(MainState.Initial)
    val uiState: StateFlow<MainState> = _uiState.asStateFlow()

    fun signIn(code: String) {
        viewModelScope.launch {
            try {
                signInUseCase(code)
                _uiState.update { MainState.NavigateToHomeFragment }
            } catch (e: Throwable) { }
        }
    }
}

sealed class MainState {
    data object Initial : MainState()
    data object NavigateToHomeFragment : MainState()
}