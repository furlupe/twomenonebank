package com.example.customerclient.ui.bill

interface BillsListener {
    fun backToMainFragment()
    fun getBillId(): String
    fun getStartBillScreenType(): String
}