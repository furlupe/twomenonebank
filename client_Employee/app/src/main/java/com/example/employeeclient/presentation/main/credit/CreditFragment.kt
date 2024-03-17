package com.example.employeeclient.presentation.main.credit

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
import com.example.employeeclient.databinding.FragmentCreditBinding
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class CreditFragment : Fragment() {

    private lateinit  var binding: FragmentCreditBinding
    private val viewModel: CreditViewModel by viewModel()

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_credit, container, false)
        binding = FragmentCreditBinding.bind(view)

        binding.btCreateTariff.setOnClickListener {
            viewModel.create(
                binding.etName.text.toString(),
                binding.etRate.text.toString().toInt()
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

                if (it.created && binding.etRate.text.isNotEmpty()) {
                    val toast = Toast.makeText(context, "Tariff created", Toast.LENGTH_LONG)
                    toast.show()
                }

                if (it.created) {
                    binding.etName.setText("", TextView.BufferType.EDITABLE)
                    binding.etRate.setText("", TextView.BufferType.EDITABLE)
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