package com.example.customerclient.ui.bottombar

import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.navigation.findNavController
import androidx.navigation.ui.setupWithNavController
import com.example.customerclient.R
import com.example.customerclient.databinding.ActivityBottomBarBinding
import com.google.android.material.bottomnavigation.BottomNavigationView

class BottomBarActivity: AppCompatActivity() {
    private lateinit var binding: ActivityBottomBarBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityBottomBarBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navView: BottomNavigationView = binding.navView

        val navController = findNavController(R.id.nav_host_fragment_activity_bottom_bar)

        navView.setupWithNavController(navController)

    }
}