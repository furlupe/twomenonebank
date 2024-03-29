package com.example.customerclient.ui.auth

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.auth.AuthorizeUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class SignInViewModel(private val authorizeUseCase: AuthorizeUseCase) : ViewModel() {

    private val _uiState: MutableStateFlow<SignInState> = MutableStateFlow(SignInState.Content)
    val uiState: StateFlow<SignInState> = _uiState.asStateFlow()

    fun authorize() {
        viewModelScope.launch {
            try {
                authorizeUseCase()
                _uiState.update { SignInState.NavigateToWebSite }
            } catch (e: Throwable) {
            }
        }
    }
}

sealed class SignInState {
    data object Content : SignInState()

    data object NavigateToWebSite : SignInState()

    data object Error : SignInState()
}