package com.example.employeeclient.presentation.auth.signin

import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentSignInBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel


class SignInFragment : Fragment() {

    private lateinit var binding: FragmentSignInBinding
    private val viewModel: SignInViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_sign_in, container, false)
        binding = FragmentSignInBinding.bind(view)

        val appLinkIntent = requireActivity().intent
        val appLinkData = appLinkIntent.data
        val code = appLinkData?.getQueryParameter("code")
        if (code != null) {
            viewModel.connect(code)
        }

        setOnButtonClickListeners()

        lifecycleScope.launch {
            viewModel.state.collect {
                if (it.redirectLink != null) {
                    val browserIntent = Intent(Intent.ACTION_VIEW, Uri.parse(it.redirectLink))
                    requireContext().startActivity(browserIntent)
                }

                if (it.isLoading) {
                    showLoading()
                    return@collect
                }


                if (it.navigateToMain) {
                    navigateToMainScreen()
                }

                hideLoading()
            }
        }

        return binding.root
    }

    private fun setOnButtonClickListeners() {
        setOnSignInButtonClickListener()
    }

    private fun setOnSignInButtonClickListener() = binding.btSignIn.setOnClickListener {
//        navigateToMainScreen()
        viewModel.authorize()
    }

    private fun navigateToMainScreen() {
        val action = SignInFragmentDirections.actionSignInFragmentToMainActivity()
        findNavController().navigate(action)
    }

    private fun showLoading() {
//        binding.tvError.isGone = true
        binding.grContent.isGone = true
        binding.loading.show()
    }

    private fun hideLoading() {
//        binding.tvError.isGone = true
        binding.grContent.isGone = false
        binding.loading.hide()
    }
}