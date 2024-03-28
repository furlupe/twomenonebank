package com.example.customerclient.ui.auth

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.auth.SignInUseCase
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class SignInViewModel(
    private val signInUseCase: SignInUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<SignInState> = MutableStateFlow(SignInState.Content)
    val uiState: StateFlow<SignInState> = _uiState.asStateFlow()
    fun signIn(username: String, password: String) {
        viewModelScope.launch {
            try {
                signInUseCase(username, password)
                withContext(Dispatchers.Main) {
                    _uiState.update { SignInState.NavigateToMainScreen }
                }
            } catch (e: Exception) {
                withContext(Dispatchers.Main) {
                    _uiState.update { SignInState.Error }
                }
            }
        }
    }
}

sealed class SignInState {
    data object Content : SignInState()

    data object NavigateToMainScreen : SignInState()

    data object Error : SignInState()
}