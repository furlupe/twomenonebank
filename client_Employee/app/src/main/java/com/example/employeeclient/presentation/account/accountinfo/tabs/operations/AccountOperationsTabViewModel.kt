package com.example.employeeclient.presentation.account.accountinfo.tabs.operations

import android.util.Log
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.employeeclient.data.remote.dto.account.event.RequestAccountHistoryBodyDto
import com.example.employeeclient.domain.repository.remote.AccountRepository
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class AccountOperationsTabViewModel(
    private val creditId: String,
    private val accountRepository: AccountRepository
) : ViewModel() {
    private val _state = MutableStateFlow(AccountOperationsTabState())
    val state: StateFlow<AccountOperationsTabState> = _state

    init {
        getCreditOperations(1)
    }

    private fun getCreditOperations(page: Int) = viewModelScope.launch {
        try {
            val events = accountRepository.getAccountOperations(creditId, RequestAccountHistoryBodyDto())
//            val items = events.items.map { event ->
//                val updatedBalanceChange = event.balanceChange?.copy(
//                    accountName = accountRepository.getAccount(event.balanceChange.accountId).name
//                )
//                val transferSourceName = event.transfer?.source?.accountId?.let {
//                    accountRepository.getAccount(it).name
//                }
//                val transferTargetName = event.transfer?.target?.accountId?.let {
//                    accountRepository.getAccount(it).name
//                }
//
//                val updatedTransferSource = transferSourceName?.let { event.transfer.source.copy(accountName = it) }
//                val updatedTransferTarget = transferTargetName?.let { event.transfer.target.copy(accountName = it) }
//
//                if (event.transfer?.id != null && updatedTransferSource != null && updatedTransferTarget != null) {
//                    event.copy(
//                        balanceChange = updatedBalanceChange,
//                        transfer = TransferDomain(
//                            event.transfer.id,
//                            updatedTransferSource,
//                            updatedTransferTarget
//                        )
//                    )
//                } else event
//            }

            Log.d("MY", "${events.items}")

            _state.update { prevState ->
                prevState.copy(
                    currentPage = events.currentPage,
                    totalPages = events.totalPages,
                    items = events.items,
                    isLastPage = page == events.totalPages
                )
            }
        } catch (exception: Exception) {
            Log.d("MY", exception.message.toString())
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
}