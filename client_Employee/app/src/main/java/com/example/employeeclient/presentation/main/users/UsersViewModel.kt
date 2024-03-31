package com.example.employeeclient.presentation.main.users

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.usecase.users.GetUsersPageUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class UsersViewModel(
    private val getUsersPageUseCase: GetUsersPageUseCase
) : ViewModel() {

    private val _state = MutableStateFlow(UsersState())
    var state: StateFlow<UsersState> = _state

    init {
        loadPage(1)
    }

    fun loadNextPage() {
        val nextPage = _state.value.currentPage + 1
        loadPage(nextPage)
    }

    fun reInit() {
        _state.update { UsersState() }
        loadPage(1)
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
