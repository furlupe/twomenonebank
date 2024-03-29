package com.example.customerclient.ui.auth

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentSignInBinding
import com.example.customerclient.ui.MainListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class SignInFragment : Fragment() {
    private lateinit var binding: FragmentSignInBinding
    private var callback: MainListener? = null

    private val viewModel: SignInViewModel by viewModel()

    override fun onAttach(context: Context) {
        callback = activity as MainListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val mainView = inflater.inflate(R.layout.fragment_sign_in, container, false)
        binding = FragmentSignInBinding.bind(mainView)


        lifecycleScope.launch {
            viewModel.uiState.collect { signInState ->
                when (signInState) {
                    SignInState.Content -> signInContent()
                    SignInState.NavigateToWebSite -> navigateToWebsite()
                    SignInState.Error -> {}
                }
            }
        }

        return binding.root
    }

    private fun signInContent() {
        binding.comeInButton.setOnClickListener {
            viewModel.authorize()
        }
    }

    private fun navigateToWebsite() {
        callback?.openWebSite()
    }
}