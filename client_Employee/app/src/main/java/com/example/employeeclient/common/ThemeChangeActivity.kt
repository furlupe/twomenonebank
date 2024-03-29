package com.example.employeeclient.common

import android.content.res.Configuration
import android.os.Bundle
import android.preference.PreferenceManager
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.app.AppCompatDelegate
import com.example.employeeclient.R
import com.example.employeeclient.common.Constants.SHARED_PREFS_THEME

open class ThemeChangeActivity: AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        val prefsValue = PreferenceManager
            .getDefaultSharedPreferences(this)
            .getInt(SHARED_PREFS_THEME, 0)

        when (prefsValue) {
            0 -> {
                AppCompatDelegate.setDefaultNightMode(
                    AppCompatDelegate.MODE_NIGHT_NO
                )
            }

            else -> {
                AppCompatDelegate.setDefaultNightMode(
                    AppCompatDelegate.MODE_NIGHT_YES
                )
            }
        }

        setTheme(R.style.AppTheme)

        super.onCreate(savedInstanceState)
    }

}