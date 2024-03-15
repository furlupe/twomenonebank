package com.example.customerclient.ui.bill

import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.navArgs
import com.example.customerclient.R
import com.example.customerclient.databinding.ActivityBillsBinding
import com.example.customerclient.ui.bill.all.AllBillsFragmentDirections

class BillsActivity : AppCompatActivity(), BillsListener {
    private lateinit var binding: ActivityBillsBinding
    private val args: BillsActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBillsBinding.inflate(layoutInflater)
        setContentView(binding.root)

        Log.d("SCREEN_TYPE", args.screenType)
        if (args.screenType == "INFO") {
            val navHostFragment =
                supportFragmentManager.findFragmentById(R.id.nav_host_fragment_activity_bills) as NavHostFragment

            val navController = navHostFragment.navController
            val action =
                AllBillsFragmentDirections.actionNavigationAllBillsToNavigationBillInfo(args.billId)

            navController.navigate(action)
        }

    }

    override fun backToMainFragment() {
        finish()
    }

    override fun getBillId(): String {
        return args.billId
    }

    override fun getStartScreenType(): String {
        return args.screenType
    }
}