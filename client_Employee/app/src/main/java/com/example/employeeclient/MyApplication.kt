package com.example.employeeclient

import android.app.Application
import com.example.employeeclient.di.appModule
import com.example.employeeclient.di.useCaseModule
import com.example.employeeclient.di.viewModelModule
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin

class MyApplication : Application() {
    override fun onCreate() {
        super.onCreate()

        startKoin {
            androidContext(this@MyApplication)
            modules(appModule, useCaseModule, viewModelModule, )
        }
    }
}