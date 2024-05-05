package com.example.employeeclient

import android.app.Application
import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.Context
import android.util.Log
import android.widget.Toast
import com.example.employeeclient.common.Constants.EMPLOYEE_TOPIC
import com.example.employeeclient.common.Constants.LOCAL_CHANNEL
import com.example.employeeclient.common.Constants.LOCAL_CHANNEL_ID
import com.example.employeeclient.di.appModule
import com.example.employeeclient.di.useCaseModule
import com.example.employeeclient.di.viewModelModule
import com.google.firebase.Firebase
import com.google.firebase.messaging.messaging
import org.koin.android.ext.koin.androidContext
import org.koin.core.context.GlobalContext.startKoin

class MyApplication : Application() {

    override fun onCreate() {
        super.onCreate()

        createNotificationChannel()

        Firebase.messaging.subscribeToTopic(EMPLOYEE_TOPIC)
            .addOnCompleteListener { task ->
                var msg = "Subscribed"
                if (!task.isSuccessful) {
                    msg = "Subscribe failed"
                }
                Log.d(TAG, msg)
                Toast.makeText(baseContext, msg, Toast.LENGTH_SHORT).show()
            }

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

    companion object {
        private val TAG = MyApplication::class.java.simpleName
    }
}