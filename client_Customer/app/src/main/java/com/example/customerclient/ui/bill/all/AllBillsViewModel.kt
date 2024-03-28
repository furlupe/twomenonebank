package com.example.customerclient.ui.bill.all

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.bill.GetUserBillsInfoFromDatabaseUseCase
import com.example.customerclient.domain.usecases.bill.GetUserBillsPagedInfoUseCase
import com.example.customerclient.ui.home.BillInfo
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class AllBillsViewModel(
    private val getUserBillsPagedInfoUseCase: GetUserBillsPagedInfoUseCase,
    private val getUserBillsInfoFromDatabaseUseCase: GetUserBillsInfoFromDatabaseUseCase
) : ViewModel() {

    private val _billsInfoState: MutableStateFlow<PagingData<BillInfo>> =
        MutableStateFlow(value = PagingData.empty())
    val billsInfoState: MutableStateFlow<PagingData<BillInfo>> get() = _billsInfoState


    fun getBills() {
        viewModelScope.launch {
            try {
                getUserBillsPagedInfoUseCase()
                    .distinctUntilChanged()
                    .cachedIn(viewModelScope)
                    .collect { bills: PagingData<BillInfo> ->
                        _billsInfoState.value = bills
                    }
            } catch (e: Exception) {
                withContext(Dispatchers.IO) {
                    val allUserBills = getUserBillsInfoFromDatabaseUseCase()
                    withContext(Dispatchers.Main) {
                        _billsInfoState.value = PagingData.Companion.from(allUserBills)
                    }
                }
            }

        }
    }
}