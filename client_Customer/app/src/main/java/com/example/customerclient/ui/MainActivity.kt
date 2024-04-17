package com.example.customerclient.ui

import android.content.Intent
import android.content.res.Configuration
import android.net.Uri
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.app.AppCompatDelegate
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.NavHostFragment
import com.example.customerclient.R
import com.example.customerclient.common.Constants.WEB_SITE_URL
import com.example.customerclient.data.storage.UserTheme
import com.example.customerclient.databinding.ActivityMainBinding
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class MainActivity : AppCompatActivity(), MainListener {

    private lateinit var binding: ActivityMainBinding
    private val viewModel: MainViewModel by viewModel()
    private var code: String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val uri: Uri? = intent.data
        code = uri?.getQueryParameter("code")

        val navHostFragment =
            supportFragmentManager.findFragmentById(R.id.nav_host_fragment_activity_auth) as NavHostFragment

        val navController = navHostFragment.navController
        if (code != null && savedInstanceState == null) {
            code?.let { viewModel.signIn(it) }
        }

        if (savedInstanceState == null) {
            when (viewModel.getUserTheme()) {
                UserTheme.LIGHT -> AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO)
                UserTheme.DARK -> AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES)
            }
        }

        lifecycleScope.launch {
            viewModel.uiState.collectLatest {
                if (it == MainState.NavigateToHomeFragment && savedInstanceState == null) navController.navigate(
                    R.id.action_navigation_sign_in_to_navigation_home
                )
            }
        }
    }

    override fun openWebSite() {
        startActivity(Intent(Intent.ACTION_VIEW, Uri.parse(WEB_SITE_URL)))
    }

    override fun swipeTheme() {
        when (resources.configuration.uiMode and Configuration.UI_MODE_NIGHT_MASK) {
            Configuration.UI_MODE_NIGHT_NO -> {
                AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES)
            }

            Configuration.UI_MODE_NIGHT_YES -> {
                AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO)
            }
        }
    }


}