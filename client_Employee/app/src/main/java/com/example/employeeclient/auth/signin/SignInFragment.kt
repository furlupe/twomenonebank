package com.example.employeeclient.auth.signin

import androidx.fragment.app.viewModels
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.lifecycle.Observer
import androidx.navigation.fragment.findNavController
import com.example.employeeclient.R
import com.example.employeeclient.auth.signin.SignInFragmentDirections
import com.example.employeeclient.databinding.FragmentSignInBinding

class SignInFragment : Fragment() {

    companion object {
        fun newInstance() = SignInFragment()
    }

    private lateinit var binding: FragmentSignInBinding
    private val viewModel: SignInViewModel by viewModels()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
    }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_sign_in, container, false)
        binding = FragmentSignInBinding.bind(view)

        setOnButtonClickListeners()

        viewModel.state.observe(viewLifecycleOwner) { state ->
            when (state) {
                is SignInState.Idle -> {
                    state.error?.let { Toast.makeText(context, it, Toast.LENGTH_LONG).show() }
                }

                SignInState.Loading -> {
                    //TODO
                }
            }
        }

        return binding.root
    }

    private fun setOnButtonClickListeners() {
        setOnSignInButtonClickListener()
    }

    private fun setOnSignInButtonClickListener() = binding.btSignIn.setOnClickListener {
        navigateToMainScreen()
    }

    private fun navigateToMainScreen() {
        val action = SignInFragmentDirections.actionSignInFragmentToMainActivity()
        findNavController().navigate(action)
    }
}