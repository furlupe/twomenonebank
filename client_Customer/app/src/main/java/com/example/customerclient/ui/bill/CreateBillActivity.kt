package com.example.customerclient.ui.bill

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.customerclient.databinding.ActivityCreateBillBinding

class CreateBillActivity : AppCompatActivity() {
    private lateinit var binding: ActivityCreateBillBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityCreateBillBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun backToMainFragment() {
        finish()
    }
}