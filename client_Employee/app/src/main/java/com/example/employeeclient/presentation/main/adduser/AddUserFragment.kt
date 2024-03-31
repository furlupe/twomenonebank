package com.example.employeeclient.presentation.main.adduser

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.core.view.isGone
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import com.example.employeeclient.R
import com.example.employeeclient.databinding.FragmentAddUserBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AddUserFragment : Fragment() {

    private lateinit var binding: FragmentAddUserBinding
    private val viewModel: AddUserViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_add_user, container, false)
        binding = FragmentAddUserBinding.bind(view)

        binding.btRegister.setOnClickListener {
            val userRoles = mutableListOf<Int>()
            if (binding.cbUser.isChecked) userRoles.add(2)
            if (binding.cbEmployee.isChecked) userRoles.add(1)

            viewModel.register(
                binding.etUsername.text.toString(),
                binding.etEmail.text.toString(),
                binding.etPassword.text.toString(),
                binding.etPhone.text.toString(),
                userRoles
            )
        }

        lifecycleScope.launch {
            viewModel.state.collect {
                if (it.isLoading) {
                    showLoading()
                    return@collect
                }

                hideLoading()

                if (it.error.isNotEmpty()) {
                    val toast = Toast.makeText(context, it.error, Toast.LENGTH_LONG)
                    toast.show()
                }

                if (it.registered && binding.etEmail.text.isNotEmpty()) {
                    val toast = Toast.makeText(context, "User registered", Toast.LENGTH_LONG)
                    toast.show()
                }

                if (it.registered) {
                    binding.etUsername.setText("", TextView.BufferType.EDITABLE)
                    binding.etEmail.setText("", TextView.BufferType.EDITABLE)
                    binding.etPassword.setText("", TextView.BufferType.EDITABLE)
                    binding.etPhone.setText("", TextView.BufferType.EDITABLE)
                    binding.cbUser.isChecked = true
                    binding.cbEmployee.isChecked = false
                }
            }
        }

        return binding.root
    }

    private fun showLoading() {
        binding.content.isGone = true
        binding.loading.show()
    }

    private fun hideLoading() {
        binding.content.isGone = false
        binding.loading.hide()
    }
}