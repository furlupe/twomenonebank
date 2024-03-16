package com.example.customerclient.ui.auth.signup

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.customerclient.R
import com.example.customerclient.databinding.FragmentSignUpBinding
import org.koin.androidx.viewmodel.ext.android.viewModel

class SignUpFragment : Fragment() {
    private lateinit var binding: FragmentSignUpBinding


    private val viewModel: SignUpViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val mainView = inflater.inflate(R.layout.fragment_sign_up, container, false)
        binding = FragmentSignUpBinding.bind(mainView)

        binding.registerButton.setOnClickListener {
            navigateToBottomBarActivity()
        }

        binding.iHaveAccButton.setOnClickListener {
            navigateToSignInFragment()
        }

        return binding.root
    }

    private fun navigateToBottomBarActivity() {
        findNavController().navigate(R.id.action_navigation_sign_up_to_bottom_bar_navigation)
    }

    private fun navigateToSignInFragment() {
        findNavController().navigate(R.id.action_navigation_sign_up_to_sign_in)
    }
}