package com.example.customerclient.ui.bill.all

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import androidx.paging.PagingData
import androidx.paging.cachedIn
import com.example.customerclient.domain.usecases.bill.GetUserBillsPagedInfoUseCase
import com.example.customerclient.ui.bottombar.home.BillInfo
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.distinctUntilChanged
import kotlinx.coroutines.launch

class AllBillsViewModel(
    private val getUserBillsPagedInfoUseCase: GetUserBillsPagedInfoUseCase,
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

            }

        }
    }
}