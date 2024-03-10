package com.example.customerclient.ui.credit

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.customerclient.databinding.ActivityAllCreditsBinding

class AllCreditsActivity : AppCompatActivity() {
    private lateinit var binding: ActivityAllCreditsBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityAllCreditsBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun backToMainFragment() {
        finish()
    }
}