package com.example.employeeclient.presentation.auth.signin

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.usecase.auth.ConnectUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class SignInViewModel(
    private val connectUseCase: ConnectUseCase
) : ViewModel() {

    private val _state = MutableStateFlow(SignInState())
    val state: StateFlow<SignInState> = _state

    fun connect(login: String, password: String) = viewModelScope.launch {
        connectUseCase(login, password)

        _state.update { prevState ->
            prevState.copy(navigateToMain = true)
        }
    }
}