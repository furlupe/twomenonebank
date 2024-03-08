package com.example.customerclient.ui.bill

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.navArgs
import com.example.customerclient.databinding.ActivityBillInfoBinding

class BillInfoActivity : AppCompatActivity() {
    private lateinit var binding: ActivityBillInfoBinding
    private val args: BillInfoActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        binding = ActivityBillInfoBinding.inflate(layoutInflater)
        setContentView(binding.root)
    }

    fun getCurrentBillId(): String {
        return args.billId
    }

    fun backToMainFragment() {
        finish()
    }
}