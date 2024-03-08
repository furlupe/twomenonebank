package com.example.customerclient.ui.credit

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import com.example.customerclient.databinding.ActivityCreditInfoBinding

class CreditInfoActivity : AppCompatActivity() {
    private lateinit var binding: ActivityCreditInfoBinding
    private val args: CreditInfoActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityCreditInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun getCurrentCreditId(): String {
        return args.creditId
    }

    fun backToMainFragment() {
        finish()
    }
}