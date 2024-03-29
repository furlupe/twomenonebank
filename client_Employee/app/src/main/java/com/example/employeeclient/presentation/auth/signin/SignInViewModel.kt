package com.example.employeeclient.presentation.auth.signin

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.data.remote.network.RedirectException
import com.example.employeeclient.domain.usecase.auth.AuthorizeUseCase
import com.example.employeeclient.domain.usecase.auth.ConnectUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class SignInViewModel(
    private val authorizeUseCase: AuthorizeUseCase,
    private val connectUseCase: ConnectUseCase
) : ViewModel() {

    private val _state = MutableStateFlow(SignInState())
    val state: StateFlow<SignInState> = _state

    fun connect(code: String) = viewModelScope.launch {
        connectUseCase(code)

        _state.update { prevState ->
            prevState.copy(navigateToMain = true)
        }
    }

    fun authorize() = viewModelScope.launch {
        _state.update { prevState ->
            prevState.copy(isLoading = true)
        }

        try {
            authorizeUseCase()
        } catch (ex: RedirectException) {
            _state.update { prevState ->
                prevState.copy(
                    redirectLink = ex.redirectUrl
                )
            }
        }
    }
}