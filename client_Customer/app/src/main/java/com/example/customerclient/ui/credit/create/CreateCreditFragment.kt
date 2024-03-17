package com.example.customerclient.ui.credit.create

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentCreateCreditBinding
import com.example.customerclient.ui.credit.CreditsListener
import com.example.customerclient.ui.credit.create.components.TariffsPagingRecyclerAdapter
import kotlinx.coroutines.flow.collectLatest
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class CreateCreditFragment : Fragment() {
    private lateinit var binding: FragmentCreateCreditBinding
    private var callback: CreditsListener? = null

    private val viewModel: CreateCreditViewModel by viewModel()

    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentCreateCreditBinding.inflate(inflater, container, false)

        binding.backAllCreditsButton.setOnClickListener {
            callback?.backToMainFragment()
        }

        val tariffRecyclerView = binding.allTariffRecyclerView
        tariffRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)

        val adapter = TariffsPagingRecyclerAdapter { viewModel.onTariffClick(it) }
        context?.let { tariffRecyclerView.adapter = adapter }

        lifecycleScope.launch {
            viewModel.tariffsState.collectLatest {
                adapter.submitData(it)
            }
        }

        lifecycleScope.launch {
            viewModel.uiState.collect { createCreditState ->
                when (createCreditState) {
                    is CreateCreditState.Content -> tariffFragmentContent(createCreditState.currentTariffId) { tariffId, amount, days ->
                        viewModel.createCredit(
                            tariffId,
                            amount.toInt(),
                            days.toInt()
                        )
                    }

                    is CreateCreditState.NavigateToMainScreen -> callback?.backToMainFragment()
                    else -> {}
                }

            }
        }

        return binding.root
    }

    private fun tariffFragmentContent(
        currentTariffId: String?,
        onCreateCredit: (String, String, String) -> Unit
    ) {
        if (currentTariffId == null) {
            binding.allTariffRecyclerView.visibility = View.VISIBLE
            binding.chooseTariffTitle.visibility = View.VISIBLE

            binding.amountOfCreditTitle.visibility = View.GONE
            binding.editTextText.visibility = View.GONE
            binding.textView.visibility = View.GONE
            binding.button.visibility = View.GONE
            binding.countOfDays.visibility = View.GONE
        } else {
            binding.amountOfCreditTitle.visibility = View.VISIBLE
            binding.editTextText.visibility = View.VISIBLE
            binding.textView.visibility = View.VISIBLE
            binding.button.visibility = View.VISIBLE
            binding.countOfDays.visibility = View.VISIBLE

            binding.allTariffRecyclerView.visibility = View.GONE
            binding.chooseTariffTitle.visibility = View.GONE

            binding.button.setOnClickListener {
                onCreateCredit(
                    currentTariffId,
                    binding.editTextText.text.toString(),
                    binding.countOfDays.text.toString()
                )
            }
        }
    }
}