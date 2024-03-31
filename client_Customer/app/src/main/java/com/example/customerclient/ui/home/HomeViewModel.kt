package com.example.customerclient.ui.home

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl
import com.example.customerclient.domain.usecases.bill.AddHideBillUseCase
import com.example.customerclient.domain.usecases.bill.GetHideBillsUseCase
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
    private val getHideBillsUseCase: GetHideBillsUseCase,
    private val addHideBillUseCase: AddHideBillUseCase,
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
                _uiState.update { (_uiState.value as HomeState.Content).copy(userName = userInfo.name) }
            } catch (e: Throwable) {
            }
        }

        viewModelScope.launch(Dispatchers.IO) {
            try {
                val billsIdsInHide = getHideBillsUseCase()
                val billsInfo = getUserBillsInfoUseCase().filter { it.id !in billsIdsInHide }
                saveUserBillInfoToDatabaseUseCase(billsInfo)
                withContext(Dispatchers.IO) {
                    _uiState.update {
                        (_uiState.value as HomeState.Content).copy(
                            billsInfo = if (billsInfo.size > 2) billsInfo.slice(0..1)
                            else billsInfo
                        )
                    }
                }
            } catch (e: Throwable) {
                withContext(Dispatchers.IO) {
                    val billsInfo = getUserBillsInfoFromDatabaseUseCase()
                    withContext(Dispatchers.Main) {
                        _uiState.update {
                            (_uiState.value as HomeState.Content).copy(
                                billsInfo = if (billsInfo.size > 2) billsInfo.slice(
                                    0..1
                                ) else billsInfo
                            )
                        }
                    }
                }
            }
        }

        viewModelScope.launch {
            try {
                val creditsInfo = getUserCreditsInfoUseCase().filter { !it.isClosed }
                withContext(Dispatchers.IO) { saveUserCreditInfoToDatabaseUseCase(creditsInfo) }

                _uiState.update {
                    (_uiState.value as HomeState.Content).copy(
                        creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                    )
                }

            } catch (e: Throwable) {
                withContext(Dispatchers.IO) {
                    val creditsInfo = getUserCreditsInfoFromDatabaseUseCase()
                    withContext(Dispatchers.Main) {
                        _uiState.update {
                            (_uiState.value as HomeState.Content).copy(
                                creditsInfo = if (creditsInfo.size > 2) creditsInfo.slice(0..1) else creditsInfo
                            )
                        }
                    }
                }

            }
        }

        viewModelScope.launch {
            try {
                val creditRate = getUserCreditRateUseCase()
                _uiState.update {
                    (_uiState.value as HomeState.Content).copy(userCreditRate = "Ваш кредитный рейтинг: $creditRate")
                }
            } catch (e: Throwable) {
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

    fun createBill(name: String, currency: String) {
        viewModelScope.launch {
            try {
                openBillUseCase(name, currency)
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