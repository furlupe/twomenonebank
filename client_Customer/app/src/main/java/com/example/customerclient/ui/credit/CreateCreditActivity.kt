package com.example.customerclient.ui.credit

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.customerclient.databinding.ActivityCreateCreditBinding

class CreateCreditActivity : AppCompatActivity() {
    private lateinit var binding: ActivityCreateCreditBinding


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityCreateCreditBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun backToMainFragment() {
        finish()
    }
}