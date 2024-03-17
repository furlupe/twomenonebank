package com.example.employeeclient.presentation.main.adduser

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.model.user.RegisterInfoDomain
import com.example.employeeclient.domain.usecase.db.user.InsertUserUseCase
import com.example.employeeclient.domain.usecase.users.RegisterUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class AddUserViewModel(
    private val insertUserUseCase: InsertUserUseCase,
    private val registerUseCase: RegisterUseCase
) : ViewModel() {

    private val _state = MutableStateFlow(AddUserState())
    var state: StateFlow<AddUserState> = _state

    fun register(
        username: String,
        email: String,
        password: String,
        role: Int
    ) = viewModelScope.launch {
        val body = RegisterInfoDomain(
            username, email, password, role
        )

        _state.update {
            it.copy(isLoading = true, registered = false, error = "")
        }

        try {
            registerUseCase(body)

            _state.update { it.copy(isLoading = false, registered = true, error = "") }
        } catch (ex: Exception) {
            insertUserUseCase(body)

            _state.update {
                it.copy(
                    isLoading = false,
                    registered = false,
                    error = ex.message ?: "Registration error"
                )
            }
        }
    }
}