package com.example.employeeclient.auth.signin

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class SignInViewModel : ViewModel() {

    private val _state = MutableLiveData<SignInState>()
    val state: LiveData<SignInState> get() = _state

    init {
        _state.value = SignInState()
    }
}