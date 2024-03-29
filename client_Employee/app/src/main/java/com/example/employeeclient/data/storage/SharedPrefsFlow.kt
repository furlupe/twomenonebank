package com.example.employeeclient.data.storage

import android.content.SharedPreferences
import kotlinx.coroutines.channels.Channel
import kotlinx.coroutines.channels.awaitClose
import kotlinx.coroutines.flow.buffer
import kotlinx.coroutines.flow.callbackFlow

fun SharedPreferences.getFloatFlowForKey(keyForFloat: String) = callbackFlow<Float> {
    val listener = SharedPreferences.OnSharedPreferenceChangeListener { _, key ->
        if (keyForFloat == key) {
            trySend(getFloat(key, 0f))
        }
    }
    registerOnSharedPreferenceChangeListener(listener)
    if (contains(keyForFloat)) {
        send(getFloat(keyForFloat, 0f)) // if you want to emit an initial pre-existing value
    }
    awaitClose { unregisterOnSharedPreferenceChangeListener(listener) }
}.buffer(Channel.UNLIMITED) // so trySend never fails