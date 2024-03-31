package com.example.employeeclient.presentation.credit.creditslist.list

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.domain.model.db.BanDomain
import com.example.employeeclient.domain.repository.remote.CreditRepository
import com.example.employeeclient.domain.usecase.db.ban.InsertBanUseCase
import com.example.employeeclient.domain.usecase.users.BanUserUseCase
import com.example.employeeclient.domain.usecase.users.GetUserByIdUseCase
import com.example.employeeclient.domain.usecase.users.UnbanUserUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class CreditsListViewModel(
    private val userId: String,
    private val banUserUseCase: BanUserUseCase,
    private val unbanUserUseCase: UnbanUserUseCase,
    private val getUserByIdUseCase: GetUserByIdUseCase,
    private val creditRepository: CreditRepository,
    private val insertBanUseCase: InsertBanUseCase,
) : ViewModel() {

    private val _state = MutableStateFlow(CreditListState(userId))
    val state: StateFlow<CreditListState> = _state

    init {
        getUserInfo()
        getUserCredits(1)
    }

    private fun getUserCredits(page: Int) = viewModelScope.launch {
        try {
            val credits = creditRepository.getAllCredits(userId, page)

            _state.update { prevState ->
                prevState.copy(
                    currentPage = credits.currentPage,
                    totalPages = credits.totalPages,
                    items = credits.items,
                    isLastPage = page == credits.totalPages
                )
            }
        } catch (exception: Exception) {
            if (exception.message == "HTTP 404 Not Found") {
                _state.update { prevState ->
                    prevState.copy(
                        items = emptyList(),
                        isLastPage = true
                    )
                }
            }
        }
    }

    fun banUnbanUser() = viewModelScope.launch {
        if (_state.value.isBanned) {
            unbanUser()
        } else {
            banUser()
        }
    }

    private fun banUser() = viewModelScope.launch {
        try {
            banUserUseCase(_state.value.userId)

            _state.update { prevState ->
                prevState.copy(
                    isBanned = true
                )
            }
        } catch(ex: Exception) {
            insertBanUseCase(BanDomain(_state.value.userId, true))
        }
    }

    private fun unbanUser() = viewModelScope.launch {
        try {
            unbanUserUseCase(_state.value.userId)

            _state.update { prevState ->
                prevState.copy(
                    isBanned = false
                )
            }
        } catch (ex: Exception) {
            insertBanUseCase(BanDomain(_state.value.userId, false))
        }
    }

    private fun getUserInfo() = viewModelScope.launch {
        val user = getUserByIdUseCase(userId)

        _state.update {
            it.copy(
                userId = userId,
                userName = user.name,
                isBanned = user.isBanned,
                userCreditRating = user.creditRating
            )
        }
    }
}