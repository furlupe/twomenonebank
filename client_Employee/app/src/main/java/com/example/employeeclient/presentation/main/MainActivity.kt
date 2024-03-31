package com.example.employeeclient.presentation.main

import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.navigation.findNavController
import androidx.navigation.ui.setupWithNavController
import com.example.employeeclient.R
import com.example.employeeclient.common.ThemeChangeActivity
import com.example.employeeclient.databinding.ActivityMainBinding
import com.google.android.material.bottomnavigation.BottomNavigationView

class MainActivity : ThemeChangeActivity() {

    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
//        setTheme(R.style.AppTheme)

        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        val navView: BottomNavigationView = binding.navView
        val navController = findNavController(R.id.nav_host_fragment_activity_main)
        navView.setupWithNavController(navController)
    }

    fun recreateFragment(fragment : Fragment){
        supportFragmentManager.beginTransaction().detach(fragment).commitNow()
        supportFragmentManager.beginTransaction().attach(fragment).commitNow()
    }
}