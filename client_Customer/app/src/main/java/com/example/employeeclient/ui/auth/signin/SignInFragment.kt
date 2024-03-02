package com.example.employeeclient.ui.auth.signin

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.navigation.fragment.findNavController
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentSignInBinding

class SignInFragment : Fragment() {
    private lateinit var binding: FragmentSignInBinding

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val mainView = inflater.inflate(R.layout.fragment_sign_in, container, false)
        binding = FragmentSignInBinding.bind(mainView)

        binding.signInButton.setOnClickListener {
            navigateToAuthorizationActivity()
        }

        return binding.root
    }

    private fun navigateToAuthorizationActivity() {
        findNavController().navigate(R.id.action_navigation_sign_in_to_bottom_bar_navigation)
    }

}