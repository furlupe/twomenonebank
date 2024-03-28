package com.example.customerclient.ui

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.customerclient.data.repository.SharedPreferencesRepositoryImpl
import com.example.customerclient.data.storage.UserTheme
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class MainViewModel(
    private val sharedPreferencesRepositoryImpl: SharedPreferencesRepositoryImpl
) : ViewModel() {
    private val _theme: MutableStateFlow<UserTheme> = MutableStateFlow(UserTheme.LIGHT)
    val theme: StateFlow<UserTheme> = _theme.asStateFlow()

    init {
        viewModelScope.launch {
            withContext(Dispatchers.IO) {
                val theme = sharedPreferencesRepositoryImpl.getUserTheme()
                withContext(Dispatchers.Main) {
                    _theme.update { theme }
                }
            }
        }
    }
}