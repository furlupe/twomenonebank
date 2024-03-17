package com.example.customerclient.ui.auth.signin

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentSignInBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class SignInFragment : Fragment() {
    private lateinit var binding: FragmentSignInBinding

    private val viewModel: SignInViewModel by viewModel()
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
                    SignInState.NavigateToMainScreen -> navigateToBottomBarActivity()
                    SignInState.Error -> {}
                }
            }
        }

        return binding.root
    }

    private fun signInContent() {
        binding.comeInButton.setOnClickListener {
            viewModel.signIn(
                username = binding.emailEditText.text.toString(),
                password = binding.passwordEditText.text.toString()
            )
        }
    }

    private fun navigateToBottomBarActivity() {
        findNavController().navigate(R.id.action_navigation_sign_in_to_bottom_bar_navigation)
    }
}