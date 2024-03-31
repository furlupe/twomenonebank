package com.example.customerclient.ui.home

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.domain.usecases.bill.OpenBillUseCase
import com.example.customerclient.domain.usecases.bill.SaveUserBillInfoToDatabaseUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditRateUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.credit.GetUserCreditsInfoUseCase
import com.example.customerclient.domain.usecases.credit.SaveUserCreditInfoToDatabaseUseCase
import com.example.customerclient.domain.usecases.user.GetUserInfoUseCase
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class HomeViewModel(
    private val getUserInfoUseCase: GetUserInfoUseCase,
    private val getUserBillsInfoUseCase: GetUserBillsInfoUseCase,
    private val getUserCreditsInfoUseCase: GetUserCreditsInfoUseCase,
    private val openBillUseCase: OpenBillUseCase,
    private val getUserBillsInfoFromDatabaseUseCase: GetUserBillsInfoFromDatabaseUseCase,
    private val saveUserBillInfoToDatabaseUseCase: SaveUserBillInfoToDatabaseUseCase,
    private val getUserCreditsInfoFromDatabaseUseCase: GetUserCreditsInfoFromDatabaseUseCase,
    private val saveUserCreditInfoToDatabaseUseCase: SaveUserCreditInfoToDatabaseUseCase,
    private val getUserCreditRateUseCase: GetUserCreditRateUseCase,
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl,
) : ViewModel() {

    private val _uiState: MutableStateFlow<HomeState> = MutableStateFlow(HomeState.Content())
    val uiState: StateFlow<HomeState> = _uiState.asStateFlow()

    fun getUserBillsAndCreditsInfo() {
        viewModelScope.launch {
            try {
                val userInfo = getUserInfoUseCase()
                val billsInfo = getUserBillsInfoUseCase()
                val creditRate = getUserCreditRateUseCase()
                //val creditsInfo = getUserCreditsInfoUseCase().filter { !it.isClosed }

                withContext(Dispatchers.IO) {
                    saveUserBillInfoToDatabaseUseCase(billsInfo)
                    //saveUserCreditInfoToDatabaseUseCase(creditsInfo)
                }

                _uiState.update {
                    HomeState.Content(
                        userName = userInfo.name,
                        userCreditRate = "Ваш кредитный рейтинг: $creditRate",
                        billsInfo = if (billsInfo.size > 2) billsInfo.slice(0..1) else billsInfo,
                        //creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                    )
                }

            } catch (e: Throwable) {
                withContext(Dispatchers.IO) {
                    val billsInfo = getUserBillsInfoFromDatabaseUseCase()
                    val creditsInfo = getUserCreditsInfoFromDatabaseUseCase()
                    withContext(Dispatchers.Main) {
                        _uiState.update {
                            HomeState.Content(
                                billsInfo = if (billsInfo.size > 2) billsInfo.slice(0..1) else billsInfo,
                                creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                            )
                        }
                    }
                }

            }
        }
    }

    fun swipeMode() {
        viewModelScope.launch {
            try {
                withContext(Dispatchers.IO) {
                    sharedPreferencesRepositoryImpl.swipeUserTheme()
                }

            } catch (e: Throwable) {
            }
        }
    }

    fun createBill(name: String) {
        viewModelScope.launch {
            try {
                openBillUseCase(name)
                getUserBillsAndCreditsInfo()
            } catch (e: Exception) {

            }
        }
    }
}

sealed class HomeState {
    data class Content(
        val userName: String = "",
        val userCreditRate: String = "",
        val billsInfo: List<BillInfo> = listOf(),
        val creditsInfo: List<CreditShortInfo> = listOf(),
        val fromDatabase: Boolean = false,
    ) : HomeState()

    data object Loading : HomeState()

    data class Error(val errorMessage: String) : HomeState()

}


data class BillInfo(
    val id: String = "",
    val name: String = "",
    val balance: String = "",
    val type: String = "",
    val duration: String = ""
)

data class CreditShortInfo(
    val id: String,
    val type: String? = "",
    val balance: String,
    val date: String,
    val nextFee: String,
    val isClosed: Boolean
)