package com.example.customerclient.ui

import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.content.res.Configuration
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.util.Log
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.app.AppCompatDelegate
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.NavHostFragment
import com.example.customerclient.R
import com.example.customerclient.common.Constants.WEB_SITE_URL
import com.example.customerclient.data.storage.UserTheme
import com.example.customerclient.databinding.ActivityMainBinding
import com.google.android.gms.tasks.OnCompleteListener
import com.google.firebase.messaging.FirebaseMessaging
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel


class MainActivity : AppCompatActivity(), MainListener {

    private lateinit var binding: ActivityMainBinding
    private val viewModel: MainViewModel by viewModel()
    private var code: String? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        requestNotificationPermission()


        FirebaseMessaging.getInstance().token.addOnCompleteListener(OnCompleteListener { task ->
            if (!task.isSuccessful) {
                Log.w("MY", "Fetching FCM registration token failed", task.exception)
                return@OnCompleteListener
            }

            // Get new FCM registration token
            val token = task.result
            viewModel.saveUserToken(token)

            // Log and toast
            Log.d("MY", token)
        })

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

    private fun requestNotificationPermission() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.TIRAMISU) {
            val hasPermission = ContextCompat.checkSelfPermission(
                this,
                Manifest.permission.POST_NOTIFICATIONS
            ) == PackageManager.PERMISSION_GRANTED

            if (!hasPermission) {
                ActivityCompat.requestPermissions(
                    this,
                    arrayOf(Manifest.permission.POST_NOTIFICATIONS),
                    0
                )
            }
        }
    }


}