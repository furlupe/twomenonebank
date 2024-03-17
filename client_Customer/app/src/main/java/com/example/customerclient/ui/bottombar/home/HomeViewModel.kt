package com.example.customerclient.ui.bottombar.home

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.usecases.user.GetUserInfoUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class HomeViewModel(
    private val getUserInfoUseCase: GetUserInfoUseCase,
    private val getUserBillsInfoUseCase: GetUserBillsInfoUseCase,
    private val getUserCreditsInfoUseCase: GetUserCreditsInfoUseCase,
    private val getEuroExchangeRateUseCase: GetEuroExchangeRateUseCase,
    private val getDollarExchangeRateUseCase: GetDollarExchangeRateUseCase,
) : ViewModel() {

    private val _uiState: MutableStateFlow<HomeState> = MutableStateFlow(HomeState.Loading)
    val uiState: StateFlow<HomeState> = _uiState.asStateFlow()

    fun getUserBillsAndCreditsInfo() {
        viewModelScope.launch {
            try {
                val userId = 0.toString()

                val userInfo = getUserInfoUseCase()
                val billsInfo = getUserBillsInfoUseCase(userId)
                val creditsInfo = getUserCreditsInfoUseCase().filter { !it.isClosed }
                val euroExchangeRate = getEuroExchangeRateUseCase()
                val dollarExchangeRate = getDollarExchangeRateUseCase()

                _uiState.update {
                    HomeState.Content(
                        userName = userInfo.name,
                        euroExchangeRate = "Евро: $euroExchangeRate$",
                        dollarExchangeRate = "Доллар: $dollarExchangeRate$",
                        billsInfo = if (billsInfo.size > 2) billsInfo.slice(0..1) else billsInfo,
                        creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                    )
                }
            } catch (e: Exception) {

            }
        }
    }

    fun createBill() {}
}

sealed class HomeState {
    data class Content(
        val userName: String = "",
        val euroExchangeRate: String = "",
        val dollarExchangeRate: String = "",
        val billsInfo: List<BillInfo> = listOf(),
        val creditsInfo: List<CreditShortInfo>,
    ) : HomeState()

    data object Loading : HomeState()

    data class Error(val errorMessage: String) : HomeState()

}


data class BillInfo(
    val id: String,
    val number: String,
    val balance: String,
    val type: String,
    val duration: String
)

data class CreditShortInfo(
    val id: String,
    val type: String,
    val balance: String,
    val nextWithdrawDate: String,
    val nextFee: String,
    val isClosed: Boolean
)