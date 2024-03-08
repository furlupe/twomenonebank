package com.example.customerclient.ui.bill

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.customerclient.databinding.ActivityAllBillsBinding

class AllBillsActivity : AppCompatActivity() {
    private lateinit var binding: ActivityAllBillsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityAllBillsBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun backToMainFragment() {
        finish()
    }
}