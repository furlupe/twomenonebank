package com.example.customerclient.ui.credit

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.navArgs
import com.example.customerclient.R
import com.example.customerclient.databinding.ActivityCreditsBinding
import com.example.customerclient.ui.credit.all.AllCreditsFragmentDirections

class CreditsActivity : AppCompatActivity(), CreditsListener {
    private lateinit var binding: ActivityCreditsBinding
    private val args: CreditsActivityArgs by navArgs()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityCreditsBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment_activity_credits) as NavHostFragment

        val navController = navHostFragment.navController
        when (args.screenCreditType) {
            "INFO" -> {
                val action =
                    AllCreditsFragmentDirections.actionNavigationAllCreditsToNavigationCreditInfo(
                        args.creditId
                    )
                navController.navigate(action)
            }

            "CREATE" -> navController.navigate(R.id.action_navigationAllCredits_to_navigationCreateCredit)
        }
    }

    override fun backToMainFragment() {
        finish()
    }

    override fun getCreditId(): String {
        return args.creditId
    }

    override fun getStartCreditScreenType(): String {
        return args.screenCreditType
    }
}