package com.example.employeeclient

import android.app.Application
import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.Context
import com.example.employeeclient.common.Constants.LOCAL_CHANNEL
import com.example.employeeclient.common.Constants.LOCAL_CHANNEL_ID
import com.example.employeeclient.di.appModule
import com.example.employeeclient.di.useCaseModule
import com.example.employeeclient.di.viewModelModule
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin

class MyApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        createNotificationChannel()

        startKoin {
            androidContext(this@MyApplication)
            modules(appModule, useCaseModule, viewModelModule, )
        }
    }

    private fun createNotificationChannel() {
        val channel = NotificationChannel(
            LOCAL_CHANNEL_ID,
            LOCAL_CHANNEL,
            NotificationManager.IMPORTANCE_DEFAULT
        )
        // Register the channel with the system.
        val notificationManager: NotificationManager =
            getSystemService(Context.NOTIFICATION_SERVICE) as NotificationManager
        notificationManager.createNotificationChannel(channel)
    }
}