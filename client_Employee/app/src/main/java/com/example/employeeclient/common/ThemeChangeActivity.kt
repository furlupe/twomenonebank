package com.example.employeeclient.common

import android.os.Bundle
import android.preference.PreferenceManager
import androidx.appcompat.app.AppCompatActivity
import com.example.employeeclient.R
import com.example.employeeclient.common.Constants.SHARED_PREFS_THEME

open class ThemeChangeActivity: AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        val theme = PreferenceManager
            .getDefaultSharedPreferences(this)
            .getInt(SHARED_PREFS_THEME, R.style.AppTheme)

        setTheme(theme)

        super.onCreate(savedInstanceState)
    }
}