package com.example.customerclient.ui.bill.all

import android.util.Log
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.domain.usecases.bill.AddHideBillUseCase
import com.example.customerclient.domain.usecases.bill.GetHideBillsUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoUseCase
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class AllBillsViewModel(
    private val getHideBillsUseCase: GetHideBillsUseCase,
    private val addHideBillUseCase: AddHideBillUseCase,
    private val getUserBillsInfoUseCase: GetUserBillsInfoUseCase,
    private val getUserBillsInfoFromDatabaseUseCase: GetUserBillsInfoFromDatabaseUseCase
) : ViewModel() {

    private val _uiState: MutableStateFlow<AllBillState> =
        MutableStateFlow(AllBillState.Content(null))
    val uiState: StateFlow<AllBillState> = _uiState.asStateFlow()


    fun getBills() {
        viewModelScope.launch(Dispatchers.IO) {
            try {
                val hideBillsIds = getHideBillsUseCase()
                val bills = getUserBillsInfoUseCase().filter { it.id !in hideBillsIds }

                withContext(Dispatchers.Main) {
                    val billsWithoutHide = bills.filter { it.id !in hideBillsIds }
                    Log.d("BILLS", "$billsWithoutHide")
                    _uiState.update { AllBillState.Content(bills) }
                }
            } catch (e: Exception) {
                withContext(Dispatchers.IO) {
                    val allUserBills = getUserBillsInfoFromDatabaseUseCase()
                    withContext(Dispatchers.Main) {
                        _uiState.update { AllBillState.Content(allUserBills) }
                    }
                }
            }

        }
    }

    fun hideBill(billId: String) {
        viewModelScope.launch(Dispatchers.IO) {
            try {
                addHideBillUseCase(billId)
                getBills()
            } catch (e: Throwable) {
            }
        }
    }
}

sealed class AllBillState {
    data class Content(val items: List<BillInfo>?) : AllBillState()
}