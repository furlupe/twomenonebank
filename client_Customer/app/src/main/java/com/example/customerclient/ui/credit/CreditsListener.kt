package com.example.customerclient.ui.credit

interface CreditsListener {
    fun backToMainFragment()
    fun getCreditId(): String
    fun getStartCreditScreenType(): String
}