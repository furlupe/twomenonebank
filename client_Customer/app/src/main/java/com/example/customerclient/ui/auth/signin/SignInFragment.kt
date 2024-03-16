package com.example.customerclient.ui.auth.signin

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentSignInBinding
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

        binding.comeInButton.setOnClickListener {
            navigateToBottomBarActivity()
        }

        binding.registrationButton.setOnClickListener {
            navigateToSignUpFragment()
        }

        return binding.root
    }

    private fun navigateToBottomBarActivity() {
        findNavController().navigate(R.id.action_navigation_sign_in_to_bottom_bar_navigation)
    }

    private fun navigateToSignUpFragment() {
        findNavController().navigate(R.id.action_navigation_sign_in_to_sign_up)
    }

}