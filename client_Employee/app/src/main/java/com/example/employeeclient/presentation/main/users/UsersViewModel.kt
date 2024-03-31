package com.example.employeeclient.presentation.main.users

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.repository.remote.AuthRepository
import com.example.employeeclient.domain.usecase.users.GetUsersPageUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class UsersViewModel(
    private val auth: AuthRepository,
    private val getUsersPageUseCase: GetUsersPageUseCase
) : ViewModel() {

    private val _state = MutableStateFlow(UsersState())
    var state: StateFlow<UsersState> = _state

    init {
        loadPage(1)
        getUserTheme()
    }

    fun loadNextPage() {
        val nextPage = _state.value.currentPage + 1
        loadPage(nextPage)
    }

    fun reInit() {
        _state.update { UsersState(isLoading = true) }
        loadPage(1)
    }

    fun updateTheme(isDark: Boolean) = viewModelScope.launch {
        auth.updateUserTheme(isDark)
    }

    private fun getUserTheme() = viewModelScope.launch {
        val isDark = auth.getIsDarkTheme().isDark

        _state.update { prevState ->
            prevState.copy(
                isDarkTheme = isDark
            )
        }
    }

    private fun loadPage(page: Int) = viewModelScope.launch {
        val pageData = getUsersPageUseCase(page)

        if (page == 1) {
            _state.update { prevState ->
                prevState.copy(
                    isLoading = false,
                    isLastPage = page == pageData.totalPages
                )
            }
        }

        _state.update { prevState ->
            prevState.copy(
                users = pageData.users,
                currentPage = page,
                isPageLoading = false,
                isLastPage = page == pageData.totalPages
            )
        }
    }
}
