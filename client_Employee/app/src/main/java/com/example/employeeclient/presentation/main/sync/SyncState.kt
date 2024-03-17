package com.example.employeeclient.presentation.main.sync

data class SyncState(
    val isLoading: Boolean = false,
    val syncResult: Boolean = false,
    val error: String = ""
)
