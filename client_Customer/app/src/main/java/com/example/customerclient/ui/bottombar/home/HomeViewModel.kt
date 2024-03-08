package com.example.customerclient.ui.bottombar.home

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.useCases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.useCases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.useCases.exchangeRate.GetDollarExchangeRateUseCase
import com.example.customerclient.domain.useCases.exchangeRate.GetEuroExchangeRateUseCase
import com.example.customerclient.domain.useCases.user.GetUserNameUseCase
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
                    userName = "Владимир Валерьевич",
                    euroExchangeRate = "Евро: 12,12$ , 134,1$",
                    dollarExchangeRate = "Доллар: 43, 45$ , 123",
                    billsInfo = listOf(
                        BillInfo(
                            id = "0",
                            number = "****2206",
                            balance = "0,43$",
                            type = "Сберегательный счёт",
                            duration = "бессрочный"
                        ),
                        BillInfo(
                            id = "1",
                            number = "****2206",
                            balance = "0,43$",
                            type = "Сберегательный счёт",
                            duration = "бессрочный"
                        )
                    ),
                    creditsInfo = listOf()
                )
            }

            /*
            * val billInfo = HomeItem(
            infoBlockTitle = getString(R.string.title_bills),
            openAllInfoButtonTitle = getString(R.string.title_all_bills),
            infoBlocks = listOf(
                InfoBlock(
                    typeOfBill = "Сберегательный счёт",
                    countOfMoney = "0,43$",
                    billDuration = "бессрочный",
                    numberOfBill = "****2206"
                ),
                InfoBlock(
                    typeOfBill = "Сберегательный счёт",
                    countOfMoney = "0,43$",
                    billDuration = "бессрочный",
                    numberOfBill = "****2206"
                ),
            )
        )

        val creditInfo = HomeItem(
            infoBlockTitle = getString(R.string.title_credits),
            openAllInfoButtonTitle = getString(R.string.title_all_credits),
            infoBlocks = listOf(
                InfoBlock(
                    typeOfBill = "Сберегательный счёт",
                    countOfMoney = "0,43$",
                    billDuration = "бессрочный",
                    numberOfBill = "****2206"
                ),
            )
        )*/
        }
    }
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