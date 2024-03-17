package com.example.employeeclient.presentation.credit.creditslist

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import com.example.employeeclient.R
import com.example.employeeclient.databinding.ActivityCreditsListBinding

class CreditsListActivity : AppCompatActivity(), CreditsListListener {

    private lateinit var binding: ActivityCreditsListBinding
    private val args: CreditsListActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setTheme(R.style.AppTheme)
        setContentView(R.layout.activity_credits_list)
    }

    override fun getUserId(): String {
        return args.userId
    }

    override fun getUserName(): String {
        return args.username
    }
}