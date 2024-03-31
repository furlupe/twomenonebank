package com.example.employeeclient.presentation.account.accountslist

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import com.example.employeeclient.R
import com.example.employeeclient.common.ThemeChangeActivity

class AccountListActivity : ThemeChangeActivity(), AccountListListener {

    private val args: AccountListActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
//        setTheme(R.style.AppTheme)

        setContentView(R.layout.activity_account_list)
    }

    override fun getUserId(): String {
        return args.userId
    }

    override fun getUserName(): String {
        return args.username
    }
}