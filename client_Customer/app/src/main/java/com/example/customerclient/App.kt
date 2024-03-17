package com.example.customerclient

import android.app.Application
import com.example.customerclient.di.appModule
import com.example.customerclient.di.useCaseModule
import com.example.customerclient.di.viewModelModule
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin

class App : Application() {
    override fun onCreate() {
        super.onCreate()

        // Start Koin
        startKoin {
            androidContext(this@App)
            modules(appModule, useCaseModule, viewModelModule)
        }
    }
}