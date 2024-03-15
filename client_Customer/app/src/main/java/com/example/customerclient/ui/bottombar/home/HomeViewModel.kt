package com.example.customerclient.ui.bottombar.home

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.usecases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.usecases.user.GetUserNameUseCase
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch

class HomeViewModel(
    private val getUserNameUseCase: GetUserNameUseCase,
    private val getUserBillsInfoUseCase: GetUserBillsInfoUseCase,
    private val getUserCreditsInfoUseCase: GetUserCreditsInfoUseCase,
    private val getEuroExchangeRateUseCase: GetEuroExchangeRateUseCase,
    private val getDollarExchangeRateUseCase: GetDollarExchangeRateUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<HomeState> = MutableStateFlow(HomeState())
    val uiState: StateFlow<HomeState> = _uiState.asStateFlow()

    init {
        viewModelScope.launch {
            val userId = 0.toString()

            val userName = getUserNameUseCase(userId)
            val billsInfo = getUserBillsInfoUseCase(userId)
            val creditsInfo = getUserCreditsInfoUseCase(userId)
            val euroExchangeRate = getEuroExchangeRateUseCase()
            val dollarExchangeRate = getDollarExchangeRateUseCase()

            _uiState.update {
                HomeState(
                    userName = userName,
                    euroExchangeRate = "Евро: $euroExchangeRate$",
                    dollarExchangeRate = "Доллар: $dollarExchangeRate$",
                    billsInfo = if (billsInfo.size > 2) billsInfo.slice(0..1) else billsInfo,
                    creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                )
            }
        }
    }

    fun createBill() {}
    fun createCredit(amount: String) {}
}

data class HomeState(
    val userName: String = "",
    val euroExchangeRate: String = "",
    val dollarExchangeRate: String = "",
    val billsInfo: List<BillInfo> = listOf(),
    val creditsInfo: List<CreditInfo> = listOf(),
)

data class BillInfo(
    val id: String,
    val number: String,
    val balance: String,
    val type: String,
    val duration: String
)

data class CreditInfo(
    val id: String,
    val type: String,
    val balance: String,
    val nextWithdrawDate: String,
    val nextFee: String,
)