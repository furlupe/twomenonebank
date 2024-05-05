package com.example.employeeclient

import android.app.NotificationManager
import androidx.core.app.NotificationCompat
import com.example.employeeclient.common.Constants
import com.google.firebase.messaging.FirebaseMessagingService
import com.google.firebase.messaging.RemoteMessage
import java.util.Random


class PushNotificationService: FirebaseMessagingService() {

    override fun onNewToken(token: String) {
        super.onNewToken(token)
    }

    override fun onMessageReceived(message: RemoteMessage) {
        super.onMessageReceived(message)
        generateNotification(message)
    }

    private fun generateNotification(message: RemoteMessage) {
        val notification = message.notification ?: return
        val builder = NotificationCompat.Builder(this, Constants.LOCAL_CHANNEL_ID)
            .setSmallIcon(R.drawable.baseline_circle_notifications_24)
            .setContentTitle(notification.title)
            .setContentText(notification.body)
            .setPriority(notification.notificationPriority ?: NotificationCompat.PRIORITY_DEFAULT)

        val notificationManager = getSystemService(NOTIFICATION_SERVICE) as NotificationManager
        val messageId = Random().nextInt(9999 - 1000) + 1000
        notificationManager.notify(messageId, builder.build())
    }
}