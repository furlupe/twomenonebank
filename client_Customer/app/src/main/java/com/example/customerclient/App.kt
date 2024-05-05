package com.example.customerclient

import android.app.Application
import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.Context
import com.example.customerclient.common.Constants.LOCAL_CHANNEL
import com.example.customerclient.common.Constants.LOCAL_CHANNEL_ID
import com.example.customerclient.di.appModule
import com.example.customerclient.di.useCaseModule
import com.example.customerclient.di.viewModelModule
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin

class App : Application() {

    override fun onCreate() {
        super.onCreate()

        createNotificationChannel()

        startKoin {
            androidContext(this@App)
            modules(appModule, useCaseModule, viewModelModule)
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
