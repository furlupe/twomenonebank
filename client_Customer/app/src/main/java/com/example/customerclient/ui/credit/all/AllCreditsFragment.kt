package com.example.customerclient.ui.credit.all

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.fragment.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.customerclient.databinding.FragmentAllCreditsBinding
import com.example.customerclient.ui.bottombar.home.CreditInfo
import com.example.customerclient.ui.common.CreditsInfoRecyclerAdapter
import com.example.customerclient.ui.credit.CreditsListener
import kotlinx.coroutines.launch
import org.koin.androidx.viewmodel.ext.android.viewModel

class AllCreditsFragment : Fragment() {
    private lateinit var binding: FragmentAllCreditsBinding
    private var callback: CreditsListener? = null

    private val viewModel: AllCreditsViewModel by viewModel()
    override fun onAttach(context: Context) {
        callback = activity as CreditsListener
        super.onAttach(context)
    }

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentAllCreditsBinding.inflate(inflater, container, false)

        lifecycleScope.launch {
            viewModel.uiState.collect { allCreditsState -> allCreditsFragmentContent(allCreditsState.credits) }
        }

        return binding.root
    }

    private fun allCreditsFragmentContent(
        bills: List<CreditInfo>
    ) {
        // Кнопка назад
        binding.backAllCreditsButton.setOnClickListener { callback?.backToMainFragment() }

        // Список счетов
        val allBillsRecyclerView = binding.allCreditsRecyclerView
        allBillsRecyclerView.layoutManager =
            LinearLayoutManager(context, LinearLayoutManager.VERTICAL, false)
        context?.let {
            allBillsRecyclerView.adapter =
                CreditsInfoRecyclerAdapter(
                    items = bills,
                    onCreditClick = { creditId -> navigateToCreditInfoFragment(creditId) }
                )
        }
    }

    private fun navigateToCreditInfoFragment(billId: String) {
        val action =
            AllCreditsFragmentDirections.actionNavigationAllCreditsToNavigationCreditInfo(billId)
        findNavController().navigate(action)
    }
}